class Quizz {
    constructor(
        $validationBtn
        , $questionsContainer
        , $explanationContainer
        , $section
        , $questionTxtContainer
        , $validationContainer
        , $nextContainer
        , $next) {
        this.questionIndex = 0;
        this.score = 0;
        this.$validationBtn = $validationBtn;
        this.$section = $section;
        this.$questionTxtContainer = $questionTxtContainer;
        this.$questionsContainer = $questionsContainer;
        this.$explanationContainer = $explanationContainer;
        this.$validationContainer = $validationContainer;
        this.$nextContainer = $nextContainer;
        this.$next = $next;
        this.canSelect = true;

        return (async () => {
            // All async code here
            this.data = await this.initData();
            shuffleArray(this.data)
            this.question = new Question(this.data[this.questionIndex]);
            this.nbQuestions = this.data.length;
            $("#nb-questions").text(this.nbQuestions);
            return await this; // when done
        })();
    }

    async initData() {
        return await $.ajax({
            url: "data.json"
        });
    }

    setQuestion() {
        this.question = new Question(this.data[this.questionIndex]);
        if (this.question.answers.length == 0) {
            this.nextQuestion();
            $("#nb-questions").text(parseInt($("#nb-questions").text()) - 1 )
            return;
        }
        this.handleQuestionDisplayInfo(this.question.questionData);

        let that = this;

        $(".question-container").click(function () {
            that.handleQuestionSelection($(this));
        });
    }

    get getScore() {
        return this.score;
    }

    handleQuestionDisplayInfo(question) {
        this.$section.text(question.section);
        this.$questionTxtContainer.text(question.questionTxt);
        this.$explanationContainer.text(question.explanation);

        this.$questionsContainer.empty();

        for (let i = 0; i < question.questionPossibilities.length; i++) {
            let questionPossibility = question.questionPossibilities[i];

            let $questionContainer = $("<div class='question-container'></div>");

            $questionContainer
                .append("<div class='letter'>" +
                    questionPossibility.split('.')[0]
                    + "</div>");

            $questionContainer
                .append("<div class='text'>" +
                    questionPossibility.split('.')[1]
                    + "</div>");

            this.$questionsContainer.append($questionContainer)
        }
    }

    handleQuestionSelection($question) {
        if (!this.canSelect) {
            return
        }
        

        if (!this.question.isQCM) {
            if (!$question.hasClass("selected")) {
                $(".question-container").removeClass("selected");
                $question.addClass("selected");
            }
            else {
                $question.removeClass("selected");
            }
        } else {
            if (!$question.hasClass("selected")) {
                $question.addClass("selected");
            }
            else {
                $question.removeClass("selected");
            }
        }
        this.handleValiderBtn();
    }

    handleValiderBtn() {
        if ($(".question-container.selected").length > 0) {
            this.showValider();
        } else {
            this.hideValider();
        }
    }


    showExplanation() {
        let questionObj = new Question(this.data[this.questionIndex]);

        let $selectedAnswers = $(".question-container.selected");
        let answersArray = [];

        $selectedAnswers.each(function () {
            answersArray.push($(this).find('.letter').text());
        });

        if (questionObj.isCorrect(answersArray)) {
            this.$explanationContainer.addClass("vrai");
        } else {
            this.$explanationContainer.addClass("faux");
        }

        this.$explanationContainer.show();
        this.setGoodSelectedValueGreen();
        this.canSelect = false;
        this.hideValider();
        this.showNext();
    }

    hideExplanation() {
        this.$explanationContainer.hide();
        this.$explanationContainer.removeClass("faux");
    }

    nextQuestion() {
        this.hideNext();
        this.hideExplanation();

        this.questionIndex++;
        $("#no-question").text(parseInt($("#no-question").text()) + 1);
        this.setQuestion(this.questionIndex);
        this.canSelect = true;
        
    }

    showValider() {
        this.$validationContainer.show();
    }
    hideValider() {
        this.$validationContainer.hide();
    }
    showNext() {
        this.$nextContainer.show();
    }
    hideNext() {
        this.$nextContainer.hide();
    }
    setGoodSelectedValueGreen() {
        let that = this;
        this.$questionsContainer.children().each(function () {
            let letter = $(this).find('.letter').text();

            if ($(this).hasClass('selected')) {
                console.log(that.question.answers);
                if (that.question.answers.includes(letter)) {
                    $(this).addClass('correct');
                }
                else {
                    $(this).addClass('wrong');
                }
            }
            else {
                if (that.question.answers.includes(letter)) {
                    $(this).addClass('correct');
                }
            }
        });
    }


}