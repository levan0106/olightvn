(function ($, window, undefined) {
    $.lvSlider = function (element) {
        this.$el = $(element);
        this._init();
    };
    $.lvSlider.prototype = {
        _init: function () {
            // cache some elements and initialize some variables
            this._config();
            // initialize/bind the events
            this._initEvents();
        },
        _config: function () {

            // the list of items
            this.$list = this.$el.children('ul');
            // the items (li elements)
            this.$items = this.$list.children('li');
            // total number of items
            this.itemsCount = this.$items.length;

            this.$list.width((this.itemsCount * 100) + '%');

            this.current = 1;
            this.$navPrev = $('<span class="lv-prev">&lt;</span>');
            this.$navNext = $('<span class="lv-next">&gt;</span>');
            $('<nav/>').append(this.$navPrev, this.$navNext).appendTo(this.$el);

            //visible nav control
            this._navControl();

        },
        _initEvents: function () {

            var self = this;
            this.$navPrev.on('click', $.proxy(this._navigate, this, 'previous'));
            this.$navNext.on('click', $.proxy(this._navigate, this, 'next'));

        },
        _navigate: function (direction) {
            // update current values
            if (direction === 'next' && this.current < this.itemsCount) {
                ++this.current;
            }
            else if (direction === 'previous' && this.current > 1) {
                --this.current;
            }
            //visible nav control
            this._navControl();
            // slide
            this._slide();

        },
        _navControl: function () {
            this.$navPrev.show();
            this.$navNext.show();

            if (this.current == 1)
                this.$navPrev.hide();
            if (this.current >= this.itemsCount)
                this.$navNext.hide();
        },
        _slide: function () {
            this.$el.children().animate({
                left: (this.current - 1) * this.$el.width() * -1
            }, 100);
        }
    }
    $.fn.lvSlider = function () {
        new $.lvSlider(this);
    }

})(jQuery, window);