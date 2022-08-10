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
});

