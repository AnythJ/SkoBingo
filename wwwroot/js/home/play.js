function closeDetails() {
    document.getElementsByClassName("sentence-details")[0].style.display = "none";
}

window.onload = function () {
    console.log(localStorage.getItem("selectedBlocks"));

    if (localStorage.getItem("selectedBlocks") == null && localStorage.getItem("swapedBlocks") == null) {
        swapBlocks();
    }
    else if (localStorage.getItem("swapedBlocks") != null) {

        var blocks = document.getElementsByClassName("sentence-block");
        var blocksTexts = document.getElementsByClassName("sentence-text");
        var swapedBlocks = JSON.parse(localStorage.getItem("swapedBlocks") || "[]");
        var selectedBlocks = JSON.parse(localStorage.getItem("selectedBlocks") || "[]");
        for (let i = 0; i < swapedBlocks.length; i++) {
            blocks[i].onclick = function () { openDetails(String(swapedBlocks[i]), blocks[i].id); };
            blocksTexts[i].innerHTML = swapedBlocks[i];
        }

        if (localStorage.getItem("selectedBlocks") != null) {
            for (let i = 0; i < blocks.length; i++) {
                if (selectedBlocks.indexOf(String(i)) >= 0) {
                    var markBlock = document.getElementById(selectedBlocks[selectedBlocks.indexOf(String(i))]);
                    markBlock.style.backgroundColor = "var(--bg-secondary)";
                }
            }
        }
    }

    
};



function newBingo() {
    localStorage.removeItem("swapedBlocks");
    localStorage.removeItem("selectedBlocks");
    closeWinPrompt();
    location.reload();
}

function userFinish() {
    localStorage.setItem("UserFinished", JSON.stringify(true));
}


function swapBlocks() {
    var blocks = document.getElementsByClassName("sentence-block");
    var blocksTexts = document.getElementsByClassName("sentence-text");
    localStorage.setItem("UserFinished", JSON.stringify(false));

    let n = blocks.length;


    var swapedBlocks = JSON.parse(localStorage.getItem("swapedBlocks") || "[]");
    console.log(blocks.length + " " + blocksTexts.length + " length");
    
    
    while (n > 0) {
        let r = Math.floor(Math.random() * (blocks.length));
        n--;
        let tempText = blocksTexts[r].innerText;

        blocksTexts[r].innerText = blocksTexts[n].innerText;
        blocksTexts[n].innerText = tempText;
    }

    var blocksAfterSwap = document.getElementsByClassName("sentence-text");
    for (let i = 0; i < blocks.length; i++) {
        swapedBlocks.push(blocksAfterSwap[i].textContent);
    }

    for (let i = 0; i < swapedBlocks.length; i++) {
        blocks[i].onclick = function () { openDetails(String(swapedBlocks[i]), blocks[i].id); };
        blocksTexts[i].innerHTML = swapedBlocks[i];
    }

    localStorage.setItem("swapedBlocks", JSON.stringify(swapedBlocks));
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
    console.log("openDetails " + text + " " + id);
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

    for (var i = startingIndex; i < size * size; i += size) {
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

