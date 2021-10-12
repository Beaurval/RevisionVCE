// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    function displayAnswer(id) {
        $("#" + id).show();
    }

    function vrai(id) {
        $("#" + id).css('color', 'green');
        $("#current").text(parseInt($("#current").text()) + 1);
    }

    function faux(id) {
        $("#" + id).css('color', 'red');
    }

    $.getJSON("data.json", function (data) {
        for (var i = 0; i < data.length; i++) {
            let questionContent = $("<div class='mb-5' id='text-" + data[i].questionNumber  + "'></div>");


            let questionTitle = $("<h3>QUESTION " + data[i].questionNumber + "</h3>");
            let questionText = $("<p>" + data[i].questionTxt + "</p>");
            let questionNote = $("<p style='font-weight:bold'>" + data[i].questionNote + "</p>")
            let choicesListe = $("<ul></ul>");

            if (data[i].questionPossibilities != undefined) {
                for (var j = 0; j < data[i].questionPossibilities.length; j++) {
                    choicesListe.append($("<li style='font-weight:350'>" + data[i].questionPossibilities[j] + "</li>"))
                }
            }

            questionContent.append(questionTitle);
            questionContent.append(questionText);
            questionContent.append(questionNote);
            questionContent.append(choicesListe);

            
            let hiddenDiv = $("<div style='display:none' id='" + data[i].questionNumber + "'></div>");
            hiddenDiv.append($("<p ><strong>Réponse : " + data[i].answer + "</strong><br/>" + "<strong>Explanation :</strong> <br/>" + data[i].explanation + "</p>"))
            hiddenDiv.append($("<div class='row ml-1'><button data-id-cible='text-" + data[i].questionNumber + "' class='btn btn-success mr-3 vrai'>J'ai eu bon</button><button data-id-cible='text-" + data[i].questionNumber + "' class='btn btn-danger faux'>J'ai eu faux</button></div>"));
            questionContent.append($("<button data-id-cible='" + data[i].questionNumber + "' class='btn btn-primary show-answer'>Voir la réponse</button>"));
            questionContent.append(hiddenDiv);
            
            $("#questions").append(questionContent);
            $("#max").text(data.length)
        }
    }).fail(function () {
        console.log("An error has occurred.");
    }).then(() => {
        $(".show-answer").click(function () {
            displayAnswer($(this).attr("data-id-cible"));
            $(this).hide();
        });
        $(".vrai").click(function () {
            vrai($(this).attr("data-id-cible"));
            $(this).hide();
            $(".faux:visible").hide();
        })

        $(".faux").click(function () {
            faux($(this).attr("data-id-cible"));
            $(this).hide();
            $(".vrai:visible").hide();
        })
    });
});