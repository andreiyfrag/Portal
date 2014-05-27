require([
    'bforms-grid',
    'bforms-toolbar',
    'bootstrap',
    'jquery',
     'bforms-inlineQuestion'
], function () {

    var UsersIndex = function (options) {
        this.options = $.extend(true, {}, options);
        this.init();
    };

    //#region Init
    UsersIndex.prototype.init = function () {

        this.initSelectors();

        this.initToolbar();

        this.initGrid();
    };

    UsersIndex.prototype.initSelectors = function () {
        this.$grid = $('#grid');
        this.$toolbar = $('#toolbar');
    };

    UsersIndex.prototype.initGrid = function () {
        this.$grid.bsGrid({
            $toolbar: this.$toolbar,
            uniqueName: 'usersGrid',
            pagerUrl: this.options.pagerUrl,
            detailsUrl: this.options.getRowsUrl,
            beforeRowDetailsSuccess: $.proxy(this._beforeDetailsSuccessHandler, this),
            afterRowDetailsSuccess: $.proxy(this._afterDetailsSuccessHandler, this),
            filterButtons: [],
            gridActions: [{
                btnSelector: '.js-btn-delete_selected',
                handler: $.proxy(function ($rows, context) {

                    var items = context.getSelectedRows();

                    this._ajaxDelete($rows, items, this.options.deleteUrl, $.proxy(function () {
                        $rows.remove();
                        context._evOnRowCheckChange($rows);
                        if (this.$grid.find('.grid_row[data-objid]').length == 0) {
                            this.$grid.bsGrid('refresh');
                        }
                    }, this), function (response) {
                        context._pagerAjaxError(response);
                    });
                }, this),
                popover: true
            }],
            rowActions: [{
                btnSelector: '.js-btn_delete',
                url: this.options.deleteUrl,
                init: $.proxy(this._deleteHandler, this),
                context: this
            }]
        });
    };

    UsersIndex.prototype.initToolbar = function () {
        this.$toolbar.bsToolbar({
            uniqueName: 'usersToolbar',
            subscribers: [this.$grid],
        });
    };
    //#endregion

    //#region Handlers
    UsersIndex.prototype._deleteHandler = function (options, $row, context) {

        ////add popover widget
        //var $me = $row.find(options.btnSelector);
        //$me.popover({
        //    html: true,
        //    placement: 'right',
        //    content: $('.popover-content').html()
        //});

        //// add delegates to popover buttons
        //var tip = $me.data('bs.popover').tip();
        //tip.on('click', '.bs-confirm', $.proxy(function (e) {
        //    e.preventDefault();

        //    var data = [];
        //    data.push({
        //        Id: $row.data('objid')
        //    });

        //    this._ajaxDelete($row, data, options.url, function () {
        //        $row.remove();
        //    }, function (response) {
        //        context._rowActionAjaxError(response, $row);
        //    });

        //    $me.popover('hide');
        //}, this));
        //tip.on('click', '.bs-cancel', function (e) {
        //    e.preventDefault();
        //    $me.popover('hide');
        //});
    };

    UsersIndex.prototype._beforeDetailsSuccessHandler = function (e, data) {

        var $row = data.$row;

        $row.find('.js-holiday_info').bsEditable({
            url: this.options.updateUrl,
            prefix: 'x' + $row.data('objid') + '.',
            additionalData: {
                objId: $row.data('objid')
            },
            editSuccessHandler: $.proxy(function (editResponse) {
                this.$grid.bsGrid('updateRows', editResponse.RowsHtml);
            }, this)
        });
    };

    UsersIndex.prototype._afterDetailsSuccessHandler = function (e, data) {
        var $row = data.$row;
        $row.find('.js-holiday_info').bsEditable('initValidation');
    };
    //#endregion

    //#region Ajax
    UsersIndex.prototype._ajaxDelete = function ($html, data, url, success, error) {
        //var ajaxOptions = {
        //    name: '|delete|' + data,
        //    url: url,
        //    data: data,
        //    context: this,
        //    success: success,
        //    error: error,
        //    loadingElement: $html,
        //    loadingClass: 'loading'
        //};
        //$.bforms.ajax(ajaxOptions);
    };
    //#endregion

    $(document).ready(function () {
        var index = new UsersIndex(window.requireConfig.pageOptions.index);
    });

});