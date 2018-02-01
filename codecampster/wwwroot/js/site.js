// Write your JavaScript code.

// Copy to clipboard with formatting (https://stackoverflow.com/a/30905277)

function copy(selector) {
    var $temp = $("<div>");
    $("body").append($temp);
    $temp.attr("contenteditable", true)
        .html($(selector).html()).select()
        .on("focus", function () { document.execCommand("selectAll", false, null) })
        .focus();
    document.execCommand("copy");
    $temp.remove();
}
