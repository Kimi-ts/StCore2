var addTagForm = new Vue({
    el: '#newTagForm',
    data: {
        newCategory: '',
        newValue: '',
        addNew: false,
        showNewTextBox: false,
    },
    methods: {
      addNewCategory: function () {
        if (this.showNewTextBox){
          if (this.newCategory){
            this.addNew = true;
          }
        }
        else{
          this.showNewTextBox = true;
        }
      }
    }
  });