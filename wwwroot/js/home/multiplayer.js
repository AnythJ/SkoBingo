var input = document.getElementById("UniqueLink");

input.onchange = function formatLink() {
    var selection = window.getSelection().toString();
    if (selection != '') {
        return;
    }

    if (event.keyCode == 37 ||
        event.keyCode == 38 ||
        event.keyCode == 39 ||
        event.keyCode == 40) {
        return;
    }




    var inputValue = input.value;
    if (inputValue.length > 15) {
        inputValue = inputValue.substring(inputValue.lastIndexOf("/") + 1);
    }

    inputValue = inputValue.replace(/[^A-Za-z0-9]/g, "");

    input.value = inputValue.toString();

}