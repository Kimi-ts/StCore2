var addTagForm = new Vue({
    el: '#newTagForm',
    data: {
        newCategory: '',
        newValue: '',
        addNew: false
    },
    methods: {
      addNewCategory: function () {
        if (this.newCategory){
          this.addNew = true;
        }
      }
    }
  });