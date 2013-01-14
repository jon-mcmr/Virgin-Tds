/* malihu custom scrollbar plugin - http://manos.malihu.gr */
(function ($) {
    $.fn.mCustomScrollbar = function (scrollType, animSpeed, easeType, bottomSpace, draggerDimType, mouseWheelSupport, scrollBtnsSupport, scrollBtnsSpeed) {
        var id = $(this).attr("id");
        var $customScrollBox = $("#" + id + " .customScrollBox");
        var $customScrollBox_container = $("#" + id + " .customScrollBox .container");
        var $customScrollBox_content = $("#" + id + " .customScrollBox .content");
        var $dragger_container = $("#" + id + " .dragger_container");
        var $dragger = $("#" + id + " .dragger");
        var $customScrollBox_horWrapper = $("#" + id + " .customScrollBox .horWrapper");

        //get & store minimum dragger height & width (defined in css)
        if (!$customScrollBox.data("minDraggerHeight")) {
            $customScrollBox.data("minDraggerHeight", $dragger.height());
        }
        if (!$customScrollBox.data("minDraggerWidth")) {
            $customScrollBox.data("minDraggerWidth", $dragger.width());
        }

        //get & store original content height & width
        if (!$customScrollBox.data("contentHeight")) {
            $customScrollBox.data("contentHeight", $customScrollBox_container.height());
        }
        if (!$customScrollBox.data("contentWidth")) {
            $customScrollBox.data("contentWidth", $customScrollBox_container.width());
        }

        CustomScroller();

        function CustomScroller(reloadType) {
            //horizontal scrolling ------------------------------
            
                var visibleHeight = $customScrollBox.height();
                if ($customScrollBox_container.height() > visibleHeight) { //enable scrollbar if content is long
                    $dragger.css("display", "block");
                    if (reloadType != "resize" && $customScrollBox_container.height() != $customScrollBox.data("contentHeight")) {
                        $dragger.css("top", 0);
                        $customScrollBox_container.css("top", 0);
                        $customScrollBox.data("contentHeight", $customScrollBox_container.height());
                    }
                    $dragger_container.css("display", "block");
                    var totalContent = $customScrollBox_content.height();
                    var minDraggerHeight = $customScrollBox.data("minDraggerHeight");
                    var draggerContainerHeight = $dragger_container.height();

                    function AdjustDraggerHeight() {
                        if (draggerDimType == "auto") {
                            var adjDraggerHeight = Math.round(totalContent - ((totalContent - visibleHeight) * 1.3)); //adjust dragger height analogous to content
                            if (adjDraggerHeight <= minDraggerHeight) { //minimum dragger height
                                $dragger.css("height", minDraggerHeight + "px").css("line-height", minDraggerHeight + "px");
                            } else if (adjDraggerHeight >= draggerContainerHeight) {
                                $dragger.css("height", draggerContainerHeight - 10 + "px").css("line-height", draggerContainerHeight - 10 + "px");
                            } else {
                                $dragger.css("height", adjDraggerHeight + "px").css("line-height", adjDraggerHeight + "px");
                            }
                        }
                    }
                    AdjustDraggerHeight();

                    var targY = 0;
                    var draggerHeight = $dragger.height();
                    $dragger.draggable({
                        axis: "y",
                        containment: "parent",
                        drag: function (event, ui) {
                            Scroll();
                        },
                        stop: function (event, ui) {
                            DraggerRelease();
                        }
                    });

                    $dragger_container.click(function (e) {
                        var $this = $(this);
                        var mouseCoord = (e.pageY - $this.offset().top);
                        if (mouseCoord < $dragger.position().top || mouseCoord > ($dragger.position().top + $dragger.height())) {
                            var targetPos = mouseCoord + $dragger.height();
                            if (targetPos < $dragger_container.height()) {
                                $dragger.css("top", mouseCoord);
                                Scroll();
                            } else {
                                $dragger.css("top", $dragger_container.height() - $dragger.height());
                                Scroll();
                            }
                        }
                    });

                    //mousewheel
                    $(function ($) {
                        if (mouseWheelSupport == "yes") {
                            $customScrollBox.unbind("mousewheel");
                            $customScrollBox.bind("mousewheel", function (event, delta) {
                                var vel = Math.abs(delta * 10);
                                $dragger.css("top", $dragger.position().top - (delta * vel));
                                Scroll();
                                if ($dragger.position().top < 0) {
                                    $dragger.css("top", 0);
                                    $customScrollBox_container.stop();
                                    Scroll();
                                }
                                if ($dragger.position().top > $dragger_container.height() - $dragger.height()) {
                                    $dragger.css("top", $dragger_container.height() - $dragger.height());
                                    $customScrollBox_container.stop();
                                    Scroll();
                                }
                                return false;
                            });
                        }
                    });

                    //scroll
                    if (bottomSpace < 1) {
                        bottomSpace = 1; //minimum bottomSpace value is 1
                    }
                    var scrollAmount = (totalContent - (visibleHeight / bottomSpace)) / (draggerContainerHeight - draggerHeight);
                    function Scroll() {
                        var draggerY = $dragger.position().top;
                        var targY = -draggerY * Math.abs(scrollAmount);
                        var thePos = $customScrollBox_container.position().top - targY;
                        $customScrollBox_container.stop().animate({ top: "-=" + thePos }, animSpeed, easeType);
                    }
                } else { //disable scrollbar if content is short
                    $dragger.css("top", 0).css("display", "none"); //reset content scroll
                    $customScrollBox_container.css("top", 0);
                    $dragger_container.css("display", "none");
                }

            $dragger.mouseup(function () {
                DraggerRelease();
            }).mousedown(function () {
                DraggerPress();
            });

            function DraggerPress() {
                $dragger.addClass("dragger_pressed");
            }

            function DraggerRelease() {
                $dragger.removeClass("dragger_pressed");
            }
        }

        $(window).resize(function () {
            if (scrollType == "horizontal") {
                if ($dragger.position().left > $dragger_container.width() - $dragger.width()) {
                    $dragger.css("left", $dragger_container.width() - $dragger.width());
                }
            } else {
                if ($dragger.position().top > $dragger_container.height() - $dragger.height()) {
                    $dragger.css("top", $dragger_container.height() - $dragger.height());
                }
            }
            CustomScroller("resize");
        });
    };
})(jQuery);