(function ($) {
    var idClick = new Array();
    var idClickVirtual;
    $.extend($.fn, {

        PopupFilter: function (options) {
            var defaults = {
                left: 'auto',
                top: 'auto',
                zindex: '1005',
                position: 'bottom'
            }

            var options = $.extend(defaults, options);
            var jPosition = $(this).position();
            var pLeft = jPosition.left;
            var pTop = jPosition.top;
            var id = $(this)[0].attributes.href.value;
            var htmlString = '<div id=\"idDiv\"' + $(id).html() + '</div>';
            var jWidth;
            var jHeight;
            var jTop;
            var jRight;
            var jBottom;
            var jidDiv = '#idDiv';
            return this.each(function () {
                var that = $(this);
                switch (options.position) {
                    case ('left'):
                        jRight = $(this).width() + 16;
                        jTop = ($(window).height() - $(id).height()) / 2;
                        break;
                    case ('right'):
                        break;
                    case ('top'):
                        jRight = ($(window).width() - $(id).width()) / 2;
                        jBottom = $(this).height() - 16;
                        break;
                    case ('bottom'):
                        break;
                }
                $(id).css({
                    right: jRight,
                    top: jTop,
                    bottom: jBottom,
                    zIndex: options.zindex,
                    position: 'fixed'
                }).fadeIn('300').ShowOn(id);
            })
        },
        ShowOn: function (e) {
            var defaults = {
                top: 0,
                left: 0,
                background: '#fff',
                zIndex: 101,
                opacity: 0.1,
                display: 'block!important',
                position: 'fixed',
                width: '100%',
                height: '100%'
            }

            return this.each(function () {
                $('body').append('<div id=\"over\"></div>');
                $('#over').css(defaults).fadeIn('300').CloseOn(e);
            })

        },
        HideOn: function (e) {
            $(e).fadeOut('100');
            $('#over').fadeOut('100', function () {
                $('#over').remove();
            })
        },
        CloseOn: function (e) {
            $(this).click(function () {
                $().HideOn(e);
            })
        },
        ClickHighlight: function (ele) {
            return this.each(function () {
                $(this).find('.row').click(function () {
                    $('.row').removeClass(ele);
                    $(this).addClass(ele);
                    $(this).find('input:radio').prop('checked', true);
                })
            })
        },
        //////////////////////////////////////////////////////
        //$("#uiForm").Validation({
        //    rules: {
        //        uiName: {
        //            required: true,
        //            type: "specialChar",
        //            minlength: 3,
        //            value: "Nhập tên loại"
        //        }
        //    },
        //    messages: {
        //        uiName: {
        //            required: "Tên không được để trống.",
        //            type: "Không nhập ký tự đặc biệt",
        //            minlength: "Tên không được nhỏ hơn 3 ký tự"
        //        }
        //    }
        //});
        //////////////////////////////////////
        Validation: function (options) {
            var msgRequire = "This is required.";
            var msgEmail = "This is not email.";
            var msgNumber = "Input value must number.";
            var msgCompare = "Input value not similar.";
            var msgInvalid = "Invalid character.";
            var msgMaxlength = "This length is more than long.";
            var msgMinlength = "This length is more than short.";
            var msgMaxValue = "Value do not more than big";
            var msgMinValue = "Value do not more than small";
            var msgDate = "Input value do not format date.";

            var defaults = {
                rules: {
                    name: {
                        required: false,
                        value: "",
                        unAccept: '<>',
                        msg: '',
                        minvalue: 0,
                        maxvalue: 9999999,
                        minlength: 0,
                        maxlength: 9999999,
                        compare: false,
                    }
                },
                autosubmit: false,
                titleInside: false
            }
            var $this = $(this);
            var options = $.extend(defaults, options);
            return this.each(function () {
                $this.find("input[type=text],[type='password'],[type='tel'],textarea").each(function () {
                    var $name = $(this).attr("id");
                    var ePassword = options.rules[$name].password ? options.rules[$name].password : false;

                    /* class=text-caption => this class set opacity color is 0.5
                        */
                    var $value = this.value;
                    var _value = options.rules[$name].value;
                    if ($value == _value)
                        $(this).addClass("text-caption");

                    //even click on textbox
                    $(this).focusin(function () {
                        $(this).removeClass("text-caption");
                        if (ePassword == true) {
                            $(this).get(0).type = 'password';
                        }
                        if ($value == _value) {
                            this.value = "";
                        }
                    });

                    //even releave on checkbox                    
                    $(this).focusout(function () {
                        if (this.value == "" & options.titleInside) {
                            this.value = _value;
                            $(this).addClass("text-caption");
                        }
                        if (options.autosubmit == true) {
                            if (!submit(options.rules[$name], options.messages[$name], this)) {
                                event.preventDefault();  //stop submit event form
                                return false;
                            }
                        }
                    });

                });

                $this.find("input[type=submit]").click(function (event) {
                    var countValid = 0;
                    $this.find("input[type=text],[type='password'],[type='tel']").each(function () {
                        var $name = $(this).attr("id");
                        if (!submit(options.rules[$name], options.messages[$name], this)) {
                            countValid++;
                            event.preventDefault();  //stop submit event form
                        }
                    });
                    //stop submit event form if valid > 0
                    if (countValid > 0)
                        return false;
                    else
                        return true;
                })
            });
            function submit(eValidate, eMessage, eHTML) {

                //Reset message
                var _msgRequire = eMessage.required ? eMessage.required : msgRequire;
                var _msgEmail = eMessage.type ? eMessage.type : msgEmail;
                var _msgNumber = eMessage.type ? eMessage.type : msgNumber;
                var _msgInvalid = eMessage.type ? eMessage.type : msgInvalid;
                var _msgCompare = eMessage.compare ? eMessage.compare : msgCompare;
                var _msgMaxlength = eMessage.maxlength ? eMessage.maxlength : msgMaxlength;
                var _msgMinlength = eMessage.minlength ? eMessage.minlength : msgMinlength;
                var _msgMaxvalue = eMessage.maxvalue ? eMessage.maxvalue : msgMaxValue;
                var _msgMinvalue = eMessage.minvalue ? eMessage.minvalue : msgMinValue;
                var _msgDate = eMessage.date ? eMessage.date : msgDate;

                //Do that check properties have config?
                var eValue = eValidate.value ? eValidate.value : "";
                var eType = eValidate.type ? eValidate.type : "";
                var eRequired = eValidate.required ? eValidate.required : false;
                var eUnAccept = eValidate.unAccept ? eValidate.unAccept : "";
                var eMaxlength = eValidate.maxlength ? eValidate.maxlength : 999999999;
                var eMinlength = eValidate.minlength ? eValidate.minlength : 0;
                var eMaxValue = eValidate.maxvalue ? eValidate.maxvalue : 999999999;
                var eMinValue = eValidate.minvalue ? eValidate.minvalue : 0;
                var eCompare = eValidate.compare ? true : false;

                //Get element value HTML
                var $value = eHTML.value;

                //create id for message error
                var idMsg = $(eHTML).attr("id") + "msg";
                if ((!$value && eRequired) || $value == eValue) {
                    var html = "<div class=\"error\" id=" + idMsg + "></div>";
                    set_message(eHTML, idMsg, html, _msgRequire);
                    return false;
                } else {
                    if (eMaxlength < $value.length) {
                        var html = "<div class=\"error\" id=" + idMsg + "></div>";
                        set_message(eHTML, idMsg, html, _msgMaxlength);
                        return false;
                    } else if ($value.length < eMinlength) {
                        var html = "<div class=\"error\" id=" + idMsg + "></div>";
                        set_message(eHTML, idMsg, html, _msgMinlength);
                        return false;
                    }
                    $value = $value.replace(/\,/g, "");
                    var minvalue = check_number(parseInt($value, 10)) ? parseInt($value, 10) : 0;
                    if (eMaxValue < parseInt($value, 10)) {
                        var html = "<div class=\"error\" id=" + idMsg + "></div>";
                        set_message(eHTML, idMsg, html, _msgMaxvalue);
                        return false;
                    } else if (eMinValue > minvalue) {
                        var html = "<div class=\"error\" id=" + idMsg + "></div>";
                        set_message(eHTML, idMsg, html, _msgMinvalue);
                        return false;
                    }
                    if (eCompare) {
                        var html = "<div class=\"error\" id=" + idMsg + "></div>";
                        //Get value element compare
                        var valueCompare = $(get_ID(eValidate.compare)).val();
                        //Compare value both elements: "$value" is element value compare ;"valueCompare" is element value have compared
                        if ($value != valueCompare) {
                            set_message(eHTML, idMsg, html, _msgCompare);
                            return false;
                        }
                    }
                    if (eType)
                        switch (eType) {
                            case ('email'):
                                var html = "<div class=\"error\" id=" + idMsg + "></div>";
                                if (!check_email($value)) {
                                    set_message(eHTML, idMsg, html, _msgEmail);
                                    return false;
                                }
                                break;
                            case ('number'):
                                var html = "<div class=\"error\" id=" + idMsg + "></div>";
                                if (!check_number($value)) {
                                    set_message(eHTML, idMsg, html, _msgNumber);
                                    return false;
                                }
                                break;
                            case ('unAccept'):
                                var html = "<div class=\"error\" id=" + idMsg + "></div>";
                                for (var i = 0; $value.length > i; ++i) {
                                    if (find_Character($value.charAt(i), eUnAccept)) {
                                        set_message(eHTML, idMsg, html, _msgInvalid);
                                        return false;
                                    }
                                }
                                break;
                            case ('specialChar'):
                                var html = "<div class=\"error\" id=" + idMsg + "></div>";
                                for (var i = 0; $value.length > i; ++i) {
                                    if (special_Character($value.charAt(i))) {
                                        set_message(eHTML, idMsg, html, _msgInvalid);
                                        return false;
                                    }
                                }
                                break;
                            case 'date':
                                if (!check_date($value)) {
                                    var html = "<div class=\"error\" id=" + idMsg + "></div>";
                                    set_message(eHTML, idMsg, html, _msgInvalid);
                                    return false;
                                }
                                break;
                                //case (''):
                                //    break;
                                //default:
                                //    var html = "<div class=\"error\" id=" + idMsg + "></div>";
                                //    //Get value element compare
                                //    var valueCompare = $(get_ID(eType)).val();
                                //    //Compare value both elements: "$value" is element value compare ;"valueCompare" is element value have compared
                                //    if ($value != valueCompare && eType) {
                                //        set_message(eHTML, idMsg, html, _msgCompare);
                                //        return false;
                                //    }
                                //    break;
                        }
                }

                //if validate is true then remove message
                $(get_ID(idMsg)).remove();
                return true;
            }
        },
        pressEnter: function (fn) {                        //use it: $('textarea').pressEnter(function(){alert('here')})
            return this.each(function () {
                var getType = $(this).attr("type");
                if (getType == "text") {
                    $(this).bind('enterPress', fn);
                    $(this).keyup(function (e) {
                        if (e.keyCode == 13) {
                            $(this).trigger("enterPress");
                        }
                    })
                } else {
                    $(this).find("input[type=text]").each(function () {
                        $(this).bind('enterPress', fn);
                        $(this).keyup(function (e) {
                            if (e.keyCode == 13) {
                                $(this).trigger("enterPress");
                            }
                        })
                    });
                }
            });
        }

    });
    function special_Character(char) {
        var str = "!@#$%^&*()_+~';:<>,.?/`";
        if (str.indexOf(char) >= 0)
            return true;
        return false;
    }
    function find_Character(char, str) {
        if (str.indexOf(char) >= 0)
            return true;
        return false;
    }
    function get_ID(ele) {
        var id = "#" + ele;
        return id;
    }
    function check_email(e) {
        emailRegExp = /^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.([a-z]){2,4})$/;
        if (emailRegExp.test(e))
            return true;
        return false;
    }
    function check_number(e) {
        if ($.isNumeric(e))
            return true;
        return false;
    }
    function set_message(id, idMsg, htmMsg, Msg) {
        var findid = $.find(get_ID(idMsg));
        if (findid.length == 0) {
            $(id).after(htmMsg);
        }
        $(get_ID(idMsg)).html(Msg);
    }
    function check_date(txtDate) {
        var currVal = txtDate;
        if (currVal == '')
            return false;
        //Declare Regex 
        var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
        var dtArray = currVal.match(rxDatePattern); // is format OK?
        if (dtArray == null)
            return false;
        //Checks for dd/mm/yyyy format.
        dtDay = dtArray[1];
        dtMonth = dtArray[3];
        dtYear = dtArray[5];
        if (dtMonth < 1 || dtMonth > 12)
            return false;
        else if (dtDay < 1 || dtDay > 31)
            return false;
        else if ((dtMonth == 4 || dtMonth == 6 || dtMonth == 9 || dtMonth == 11) && dtDay == 31)
            return false;
        else if (dtMonth == 2) {
            var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));
            if (dtDay > 29 || (dtDay == 29 && !isleap))
                return false;
        }
        return true;
    }

})(window.jQuery);