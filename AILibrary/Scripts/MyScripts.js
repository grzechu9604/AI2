
function hideBookAlreadyExistsDiv() {
    $("div.error-book-already-id-DB").remove();
}

$('#AuthorName').on('blur', hideBookAlreadyExistsDiv);
$('#Title').on('blur', hideBookAlreadyExistsDiv);