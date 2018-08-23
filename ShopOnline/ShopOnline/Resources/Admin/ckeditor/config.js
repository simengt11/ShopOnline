/**
 * @license Copyright (c) 2003-2017, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';

    config.syntaxhighlight_lang = 'csharp';
    config.syntaxhighlight_hideControls = true;
    config.languages = 'vi';
    config.filebrowserBrowseUrl = '/Resources/Admin/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/Resources/Admin/ckfinder/ckfinder.html?Types=Images';
    config.filebrowserFlashBrowseUrl = '/Resources/Admin/ckfinder/ckfinder.html?Types=Flash';
    config.filebrowserUploadUrl = '/Resources/Admin/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=File';
    config.filebrowserImageUploadUrl = '/Data/Images';
    config.filebrowserFlashUploadUrl = '/Resources/Admin/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';

    CKFinder.setupCKEditor(null, '/Resources/Admin/ckfinder/');
};
