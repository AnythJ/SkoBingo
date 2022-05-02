function closeDetails() {
    document.getElementsByClassName("sentence-details")[0].style.display = "none";
}

function closeWinPrompt() {
    document.getElementById("winPrompt").style.display = "none";
    localStorage.setItem("IgnoreScore", JSON.stringify(true));
}

function refresh() {
    window.location.reload();
}

function markOne(id) {
    var markBlock = document.getElementById(id);
    var size = document.getElementsByClassName("sentence-block").length;
    if (window.getComputedStyle(markBlock, null).getPropertyValue("background-color") == "rgb(26, 26, 29)") {
        markBlock.style.backgroundColor = "var(--bg-secondary)";

        if (!JSON.parse(localStorage.getItem("IgnoreScore"))) AddMarkedBlock(id, Math.sqrt(size));
    }
    else {
        markBlock.style.backgroundColor = "var(--bg-primary)";
        if (!JSON.parse(localStorage.getItem("IgnoreScore"))) DeleteMarkedBlock(id, Math.sqrt(size));
    }

    closeDetails();
}

function openDetails(text, id) {
    document.getElementById("detailsText").innerText = text;
    document.getElementsByClassName("sentence-details")[0].style.display = "flex";
    document.getElementById("markButton").onclick = function () { markOne(id); };
}

function AddMarkedBlock(id, size) {
    var selectedBlocks = JSON.parse(localStorage.getItem("selectedBlocks") || "[]");

    var block = id;
    selectedBlocks.push(id);
    localStorage.setItem("selectedBlocks", JSON.stringify(selectedBlocks));

    CheckIfBingo(id, size);
}

function DeleteMarkedBlock(id) {
    var selectedBlocks = JSON.parse(localStorage.getItem("selectedBlocks") || "[]");

    var block = id;
    selectedBlocks.splice(selectedBlocks.indexOf(id), 1);
    localStorage.setItem("selectedBlocks", JSON.stringify(selectedBlocks));
}

function CheckIfBingo(id, size) {
    let startingIndex = id % size;
    let verticalCount = 0;
    let horizontalCount = 0;
    var selectedBlocks = JSON.parse(localStorage.getItem("selectedBlocks") || "[]");
    
    for (var i = startingIndex; i < size*size; i+=size) {
        if (selectedBlocks.indexOf(String(i)) >= 0) verticalCount++;
    }

    var k = id;
    while (k % size != 0) k--;

    for (var i = k; i < k + size; i++) {
        if (selectedBlocks.indexOf(String(i)) >= 0) horizontalCount++;
    }

    if (horizontalCount == size || verticalCount == size) {
        document.getElementById("winPrompt").style.display = "flex";
    }
}

