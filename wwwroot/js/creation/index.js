function sizeUp() {
    if (document.getElementById("Size").value < 10) document.getElementById("Size").value++;
}

function sizeDown() {
    if (document.getElementById("Size").value > 0) document.getElementById("Size").value--;
}