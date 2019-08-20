app.directive('loading',
    function () {
        return {
            restrict: 'E',
            replace: true,
            template: '<div class="loader-patent"><div class="loader"><span></span><span></span><span></span><span></span></div>',
            link: function (scope, element, attr) {
                scope.$watch('loading',
                    function (val) {
                        if (val)
                            $(element).show();
                        else
                            $(element).hide();
                    });
            }
        }
    });