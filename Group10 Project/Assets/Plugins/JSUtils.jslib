var plugin = {
    OpenTab : function(link)
    {
        url = Pointer_stringify(link);
        document.onpointerup = function() { //Use onpointerup for touch input compatibility
            window.open(url);
            document.onpointerup = null;
        }
    },
};
mergeInto(LibraryManager.library, plugin);