function closeDetails() {
    document.getElementsByClassName("sentence-details")[0].style.display = "none";
}

window.onload = function () {
    var url = window.location.href;
    var n = url.lastIndexOf('/');
    var gameLink = url.substring(n + 1);

    let swapedBlocksItemName = "swapedBlocks";
    swapedBlocksItemName = swapedBlocksItemName.concat(gameLink);
    let selectedBlocksItemName = "selectedBlocks";
    selectedBlocksItemName = selectedBlocksItemName.concat(gameLink);
    let showNewButton = "showNewButton";
    showNewButton = showNewButton.concat(gameLink);
    

    if (localStorage.getItem(selectedBlocksItemName) == null && localStorage.getItem(swapedBlocksItemName) == null) {
        swapBlocks();
    }
    else if (localStorage.getItem(swapedBlocksItemName) != null) {
        if (localStorage.getItem(showNewButton)) document.getElementById("newBingoButton").style.display = "inline-block";

        var blocks = document.getElementsByClassName("sentence-block");
        var blocksTexts = document.getElementsByClassName("sentence-text");
        var swapedBlocks = JSON.parse(localStorage.getItem(swapedBlocksItemName) || "[]");
        var selectedBlocks = JSON.parse(localStorage.getItem(selectedBlocksItemName) || "[]");
        
        for (let i = 0; i < swapedBlocks.length; i++) {
            blocks[i].onclick = function () { openDetails(String(swapedBlocks[i]), blocks[i].id); };
            blocksTexts[i].innerHTML = swapedBlocks[i];
        }

        if (localStorage.getItem(selectedBlocksItemName) != null) {
            for (let i = 0; i < blocks.length; i++) {
                if (selectedBlocks.indexOf(String(i)) >= 0) {
                    var markBlock = document.getElementById(selectedBlocks[selectedBlocks.indexOf(String(i))]);
                    markBlock.style.backgroundColor = "var(--bg-secondary)";
                }
            }
        }
    }


};


/**
 * Removes all localStorage items related to exact bingo and reloads page.
 */
function newBingo() {
    var url = window.location.href;
    var n = url.lastIndexOf('/');
    var gameLink = url.substring(n + 1);

    let swapedBlocksItemName = "swapedBlocks";
    swapedBlocksItemName = swapedBlocksItemName.concat(gameLink);
    let selectedBlocksItemName = "selectedBlocks";
    selectedBlocksItemName = selectedBlocksItemName.concat(gameLink);
    let ignoreScore = "IgnoreScore";
    ignoreScore = ignoreScore.concat(gameLink);
    let showNewButton = "showNewButton";
    showNewButton = showNewButton.concat(gameLink);

    localStorage.removeItem(swapedBlocksItemName);
    localStorage.removeItem(selectedBlocksItemName);

    localStorage.setItem(ignoreScore, JSON.stringify(false));
    localStorage.removeItem(showNewButton);
    closeWinPrompt();
    location.reload();
}

/**
 * Sets IgnoreScore and showNewButton items in localStorage to true, closes win prompt.
 */
function userFinish() {
    var url = window.location.href;
    var n = url.lastIndexOf('/');
    var gameLink = url.substring(n + 1);

    let ignoreScore = "IgnoreScore";
    ignoreScore = ignoreScore.concat(gameLink);
    let showNewButton = "showNewButton";
    showNewButton = showNewButton.concat(gameLink);

    localStorage.setItem(ignoreScore, JSON.stringify(true));
    closeWinPrompt();
    document.getElementById("newBingoButton").style.display = "inline-block";
    localStorage.setItem(showNewButton, JSON.stringify(true));
}

/**
 * Randomly swap blocks with text and save it in localStorage as swapedBlocks for exact bingo.
 */
function swapBlocks() {
    var blocks = document.getElementsByClassName("sentence-block");
    var blocksTexts = document.getElementsByClassName("sentence-text");
    let n = blocks.length;

    var url = window.location.href;
    var x = url.lastIndexOf('/');
    var gameLink = url.substring(x + 1);

    let swapedBlocksItemName = "swapedBlocks";
    swapedBlocksItemName = swapedBlocksItemName.concat(gameLink);

    var swapedBlocks = JSON.parse(localStorage.getItem(swapedBlocksItemName) || "[]");


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


    localStorage.setItem(swapedBlocksItemName, JSON.stringify(swapedBlocks));
}

function closeWinPrompt() {
    document.getElementById("winPrompt").style.display = "none";
}

function refresh() {
    window.location.reload();
}

/**
 * Marks or unmarks block with passed id, by changing background-color and adds it to localStorage item selectedBlocks with AddMarkedBlock function.
 * @param {number} id
 */
function markOne(id) {
    var markBlock = document.getElementById(id);
    var size = document.getElementsByClassName("sentence-block").length;
    if (window.getComputedStyle(markBlock, null).getPropertyValue("background-color") == "rgb(26, 26, 29)") {
        markBlock.style.backgroundColor = "var(--bg-secondary)";

        AddMarkedBlock(id, Math.sqrt(size));
    }
    else {
        markBlock.style.backgroundColor = "var(--bg-primary)";

        DeleteMarkedBlock(id, Math.sqrt(size));
    }

    closeDetails();
}

/**
 * Opens details popup with full sentence and button to mark/unmark.
 * @param {string} text
 * @param {number} id
 */
function openDetails(text, id) {
    var markButton = document.getElementById("markButton");
    if (window.getComputedStyle(document.getElementById(id)).getPropertyValue("background-color") == "rgb(26, 26, 29)") {
        markButton.innerText = "Mark";
        markButton.classList.remove("active");
    }
    else {
        markButton.innerText = "Unmark";
        markButton.classList.add("active");
    }

    document.getElementById("detailsText").innerText = text;
    document.getElementsByClassName("sentence-details")[0].style.display = "flex";
    console.log(window.getComputedStyle(document.getElementById(id)).getPropertyValue("background-color"));
    
    markButton.onclick = function () { markOne(id); };
}

/**
 * Saves selected block in localStorage item selectedBlocks for exact bingo.
 * @param {number} id
 * @param {number} size
 */
function AddMarkedBlock(id, size) {
    var url = window.location.href;
    var n = url.lastIndexOf('/');
    var gameLink = url.substring(n + 1);

    let selectedBlocksItemName = "selectedBlocks";
    selectedBlocksItemName = selectedBlocksItemName.concat(gameLink);
    let ignoreScore = "IgnoreScore";
    ignoreScore = ignoreScore.concat(gameLink);

    var selectedBlocks = JSON.parse(localStorage.getItem(selectedBlocksItemName) || "[]");

    var block = id;
    selectedBlocks.push(id);
    localStorage.setItem(selectedBlocksItemName, JSON.stringify(selectedBlocks));

    if (!JSON.parse(localStorage.getItem(ignoreScore))) CheckIfBingo(id, size);
}

/**
 * Removes selected block from localStorage item selectedBlocks for exact bingo.
 * @param {number} id
 */
function DeleteMarkedBlock(id) {
    var url = window.location.href;
    var n = url.lastIndexOf('/');
    var gameLink = url.substring(n + 1);

    let selectedBlocksItemName = "selectedBlocks";
    selectedBlocksItemName = selectedBlocksItemName.concat(gameLink);

    var selectedBlocks = JSON.parse(localStorage.getItem(selectedBlocksItemName) || "[]");

    var block = id;
    selectedBlocks.splice(selectedBlocks.indexOf(id), 1);

    localStorage.setItem(selectedBlocksItemName, JSON.stringify(selectedBlocks));
}

/**
 * Checks if win requirements were met by comparing amount of selected blocks to size of the bingo. If requirements were met, shows winPrompt.
 * @param {number} id
 * @param {number} size
 */
function CheckIfBingo(id, size) {
    let startingIndex = id % size;
    let verticalCount = 0;
    let horizontalCount = 0;

    var url = window.location.href;
    var n = url.lastIndexOf('/');
    var gameLink = url.substring(n + 1);

    let selectedBlocksItemName = "selectedBlocks";
    selectedBlocksItemName = selectedBlocksItemName.concat(gameLink);

    var selectedBlocks = JSON.parse(localStorage.getItem(selectedBlocksItemName) || "[]");

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

