var imagePanel = new Vue({
    el: '#imagesPanel',
    data: {},
    methods: {
        copyToClipboard: function (targetId) {
            var input = document.getElementById(targetId);
            input.select();
            document.execCommand("copy");
        }
    }
});