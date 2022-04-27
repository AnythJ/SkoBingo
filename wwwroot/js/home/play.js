function closeDetails() {
    document.getElementsByClassName("question-details")[0].style.display = "none";
}

function markOne(id) {
    var markBlock = document.getElementById(id);
    if (window.getComputedStyle(markBlock, null).getPropertyValue("background-color") == "rgb(26, 26, 29)") {
        markBlock.style.backgroundColor = "var(--bg-secondary)";
    }
    else markBlock.style.backgroundColor = "var(--bg-primary)";
}

function openDetails(text, id) {
    document.getElementById("detailsText").innerText = text;
    document.getElementsByClassName("question-details")[0].style.display = "block";
    document.getElementById("markButton").onclick = function () { markOne(id);};
}

