// Показывает карточку выбранного файла, подгружая её html с сервера без перезагрузки страницы.

document.addEventListener('DOMContentLoaded', function () {
    var list = document.getElementById('documents');
    var cardBox = document.getElementById('card');

    function showCard(item) {
        list.querySelectorAll('li').forEach(function (el) { el.classList.remove('selected'); });
        item.classList.add('selected');

        fetch('/Home/Card/' + item.dataset.id)
            .then(function (response) { return response.text(); })
            .then(function (html) { cardBox.innerHTML = html; });
    }

    list.addEventListener('click', function (event) {
        var item = event.target.closest('li');
        if (item) {
            showCard(item);
        }
    });

    var first = list.querySelector('li');
    if (first) {
        showCard(first);
    }
});
