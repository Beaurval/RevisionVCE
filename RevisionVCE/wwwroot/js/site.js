function arrayEquals(a, b) {
    return Array.isArray(a) &&
        Array.isArray(b) &&
        a.length === b.length &&
        a.every((val, index) => val === b[index]);
}

function shuffleArray(inputArray) {
    inputArray.sort(() => Math.random() - 0.5);
}

let quizz;


$(async () => {
    quizz = await new Quizz(
        $("#valider")
        , $("#questions-container")
        , $("#explanation")
        , $("#section")
        , $("#questionTxt")
        , $("#validation-container")
        , $("#next-container")
        , $("#next"));

    quizz.questionIndex = 0;
    quizz.setQuestion();

    $("#valider").click(function () {
        quizz.showExplanation(true);
    })

    $("#next").click(function () {
        quizz.nextQuestion();
    })
});
