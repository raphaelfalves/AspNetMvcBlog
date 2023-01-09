jQuery(function ($) {

    var $postBody = $('#post-body');
    var $inputCategory = $('#inputCategory');

    $postBody.redactor({
        minHeight: 355,
        placeholder: 'Add content here...',
        imageUpload: "/api/images/upload"
    });

    $('#post-form').validate({
        rules: {
            title: 'required',
            summary: 'required'
        }
    });

    $('#inputTags').tagsInput({
        height: 'auto',
        width: 'auto'
    });

    $inputCategory.autocomplete({
        source: '/Admin/AutoComplete/Categories'
    });

});