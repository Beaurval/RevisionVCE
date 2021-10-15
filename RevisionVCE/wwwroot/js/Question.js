class Question {
    constructor(questionData) {
        this.questionData = questionData;
        this.answer = questionData.answer.trim();
        this.isQCM = this.answer.trim().length > 1;
        this.answers = this.answer.split('');
    }

    isCorrect(answerArray) {
        if (arrayEquals(this.answers, answerArray)) {
            $("#score").text(parseInt($("#score").text()) + 1)
            return true;
        }
        else {
            return false;
        }
    }
}