$(document).ready(function (){
    $('#widget').hide();
    $('#search').on('keyup', function (e){
        e.preventDefault();
        let searchTerm = $(this).val();
        console.log(searchTerm)
        searchTerm = encodeURIComponent(searchTerm);
        console.log(searchTerm)
        $('#results')
            .load(`https://localhost:5001/Account/SearchAccounts?searchTerm=${searchTerm}`);
    })
});

$(document).ready(function (){
    $('#getData').on('click', function (){
        $('#widget').show();
        $.ajax({
            url: 'https://localhost:5001/feedback/getCount',
            type: 'GET',
            success: function (response) {
                $('#user').text(response.user.entityName);
                $('#usersCount').text(response.user.count);
                $('#feedback').text(response.feedback.entityName);
                $('#feedbacksCount').text(response.feedback.count);
            }
        });
    });

    $('#widgetClose').on('click', function (){
        $('#widget').hide();
    })
});

$(document).ready(function () {
    $("#feedbackForm").on("submit", function (e) {
        e.preventDefault();
        let phoneId = $("input[name=phoneId]");
        let text = $("#text");
        console.log(text.val())
        console.log(phoneId.attr('id'))
        let feedback = {
            text: text.val(),
            phoneId: phoneId.attr('id')
        };
        $.post('https://localhost:5001/Feedback/Create', feedback, function (response) {
        }).done(function (response) {
            $('#feedbacks').prepend(response);
            text.text("");
            return true;
        });
    });
    
    $('#exampleModal').on('show.bs.modal', function (event) {
        const button = $(event.relatedTarget);
        const oldText = button.attr('text');
        const username = button.attr('username');
        const feedbackId = button.attr('feedbackId');
        const modal = $(this);
        modal.find('.modal-title').text('Редактируете от имени: ' + username);
        const textarea = modal.find('.modal-body textarea');
        textarea.text(oldText);
        $('#feedback-edit-form').on('submit', function (submitEvent){
            submitEvent.preventDefault();
            $('#exampleModal').modal('hide');
            fetch('https://localhost:5001/feedback/update', {
                method: 'post',
                body: JSON.stringify({id: feedbackId, text: textarea.val()}),
                headers: {'content-type': 'application/json'}
            }).then(response => {
                console.log(response);
                return response.text();
            }).then(text => {
                $(`div[id=${feedbackId}]`).html(text);
            }).catch((error) => {
                alert(error);
            });
        });
    });
});

