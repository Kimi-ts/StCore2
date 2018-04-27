var salePanel = new Vue({
    el: '#salesCollectionArea',
    data: {
        activeItemNumber: -1,
    },
    methods: {
      setActiveItem: function(number){
        this.activeItemNumber = number;
      }
    }
  });