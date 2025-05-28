(function ($) {
    var defaults = {
        topSpacing: 0,
        bottomSpacing: 0,
        className: "is-sticky",
        wrapperClassName: "sticky-wrapper",
        center: false,
        getWidthFrom: "",
        responsiveWidth: false,
        zIndex: 1000           // Added zIndex option
    },
        $window = $(window),
        $document = $(document),
        sticked = [],
        windowHeight = $window.height(),
        // Improved scroller function with throttling to improve performance
        scroller = function () {
            // Avoid calculations if no elements are sticked
            if (sticked.length === 0) return;

            var scrollTop = $window.scrollTop(),
                documentHeight = $document.height(),
                dwh = documentHeight - windowHeight,
                extra = scrollTop > dwh ? dwh - scrollTop : 0;

            // Cache viewport dimensions for better performance
            var viewportHeight = $window.height();

            for (var i = 0; i < sticked.length; i++) {
                var s = sticked[i],
                    elementTop = s.stickyWrapper.offset().top,
                    etse = elementTop - s.topSpacing - extra;

                // Check if element is in viewport before processing
                var elementHeight = s.stickyElement.outerHeight();
                var elementBottom = elementTop + elementHeight;

                // Process only elements in or near viewport
                if (elementBottom >= scrollTop - elementHeight && elementTop <= scrollTop + viewportHeight + elementHeight) {
                    if (scrollTop <= etse) {
                        if (s.currentTop !== null) {
                            s.stickyElement
                                .css({
                                    'position': '',
                                    'top': '',
                                    'z-index': ''  // Reset z-index when unsticking
                                });
                            s.stickyElement
                                .trigger("sticky-end", [s])
                                .parent()
                                .removeClass(s.className);
                            s.currentTop = null;
                        }
                    } else {
                        var newTop =
                            documentHeight -
                            s.stickyElement.outerHeight() -
                            s.topSpacing -
                            s.bottomSpacing -
                            scrollTop -
                            extra;
                        if (newTop < 0) {
                            newTop = newTop + s.topSpacing;
                        } else {
                            newTop = s.topSpacing;
                        }
                        if (s.currentTop != newTop) {
                            s.stickyElement
                                .css({
                                    'position': 'fixed',
                                    'top': newTop,
                                    'z-index': s.zIndex || defaults.zIndex  // Apply z-index
                                });

                            if (typeof s.getWidthFrom !== "undefined") {
                                s.stickyElement.css(
                                    "width",
                                    $(s.getWidthFrom).width()
                                );
                            } else if (s.widthFromWrapper) {
                                // Use wrapper width if no specific width source specified
                                s.stickyElement.css("width", s.stickyWrapper.width());
                            }

                            s.stickyElement
                                .trigger("sticky-start", [s])
                                .parent()
                                .addClass(s.className);
                            s.currentTop = newTop;
                        }
                    }
                }
            }
        },
        // Improved resizer with debouncing
        resizer = function () {
            windowHeight = $window.height();

            for (var i = 0; i < sticked.length; i++) {
                var s = sticked[i];
                if (typeof s.getWidthFrom !== "undefined" && s.responsiveWidth === true) {
                    s.stickyElement.css("width", $(s.getWidthFrom).width());
                } else if (s.widthFromWrapper) {
                    s.stickyElement.css("width", s.stickyWrapper.width());
                }
            }
        },
        methods = {
            init: function (options) {
                var o = $.extend({}, defaults, options);
                return this.each(function () {
                    var stickyElement = $(this);

                    var stickyId = stickyElement.attr("id");
                    var wrapperId = stickyId
                        ? stickyId + "-" + defaults.wrapperClassName
                        : defaults.wrapperClassName;
                    var wrapper = $("<div></div>")
                        .attr("id", stickyId ? stickyId + "-sticky-wrapper" : null)
                        .addClass(o.wrapperClassName);
                    stickyElement.wrapAll(wrapper);

                    if (o.center) {
                        stickyElement
                            .parent()
                            .css({
                                width: stickyElement.outerWidth(),
                                marginLeft: "auto",
                                marginRight: "auto",
                            });
                    }

                    if (stickyElement.css("float") == "right") {
                        stickyElement
                            .css({ float: "none" })
                            .parent()
                            .css({ float: "right" });
                    }

                    var stickyWrapper = stickyElement.parent();
                    // Preserve original height for proper layout
                    var originalHeight = stickyElement.outerHeight();
                    stickyWrapper.css("height", originalHeight);

                    // Store whether to get width from wrapper
                    var widthFromWrapper = o.getWidthFrom === "" || o.getWidthFrom === undefined;

                    sticked.push({
                        topSpacing: o.topSpacing,
                        bottomSpacing: o.bottomSpacing,
                        stickyElement: stickyElement,
                        currentTop: null,
                        stickyWrapper: stickyWrapper,
                        className: o.className,
                        getWidthFrom: o.getWidthFrom,
                        responsiveWidth: o.responsiveWidth,
                        widthFromWrapper: widthFromWrapper,
                        zIndex: o.zIndex
                    });
                });
            },
            update: scroller,
            unstick: function (options) {
                return this.each(function () {
                    var unstickyElement = $(this);

                    var removeIdx = -1;
                    for (var i = 0; i < sticked.length; i++) {
                        if (
                            sticked[i].stickyElement.get(0) ==
                            unstickyElement.get(0)
                        ) {
                            removeIdx = i;
                        }
                    }
                    if (removeIdx != -1) {
                        sticked.splice(removeIdx, 1);
                        unstickyElement.unwrap();
                        unstickyElement.removeAttr("style");
                    }
                });
            },
        };

    // Throttle function to limit execution frequency of scroll events
    function throttle(func, wait) {
        var time = Date.now();
        return function () {
            if ((time + wait - Date.now()) < 0) {
                func();
                time = Date.now();
            }
        };
    }

    // Use throttled versions of scroll and resize handlers
    var throttledScroller = throttle(scroller, 10);  // Execute at most every 10ms
    var throttledResizer = throttle(resizer, 100);   // Execute at most every 100ms

    // Event binding with performance optimization
    if (window.addEventListener) {
        window.addEventListener("scroll", throttledScroller, { passive: true });
        window.addEventListener("resize", throttledResizer, { passive: true });
    } else if (window.attachEvent) {
        window.attachEvent("onscroll", throttledScroller);
        window.attachEvent("onresize", throttledResizer);
    }

    $.fn.sticky = function (method) {
        if (methods[method]) {
            return methods[method].apply(
                this,
                Array.prototype.slice.call(arguments, 1)
            );
        } else if (typeof method === "object" || !method) {
            return methods.init.apply(this, arguments);
        } else {
            $.error("Method " + method + " does not exist on jQuery.sticky");
        }
    };

    $.fn.unstick = function (method) {
        if (methods[method]) {
            return methods[method].apply(
                this,
                Array.prototype.slice.call(arguments, 1)
            );
        } else if (typeof method === "object" || !method) {
            return methods.unstick.apply(this, arguments);
        } else {
            $.error("Method " + method + " does not exist on jQuery.sticky");
        }
    };

    // Initial call to set up any elements that should be sticky on page load
    $(function () {
        setTimeout(scroller, 0);
    });
})(jQuery);
