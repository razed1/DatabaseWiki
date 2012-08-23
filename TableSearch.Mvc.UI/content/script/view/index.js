(function() {
  /* Begin Constants */  var ColumnDescription, ErrorContainer, SearchForm, SearchParameterName, SearchTextbox, TableDescription, createObjectToSend, createRetrieveColumnDescriptionResultMethod, createRetrieveTableDescriptionResultMethod, editColumnDescriptionMethod, hideFieldSets, runFormSetup, sendTableDescriptionEdit, setTableDescription, submitSearchFormResultBuilder;
  ErrorContainer = "#divError";
  SearchForm = "#formSearchText";
  SearchParameterName = "partialName";
  SearchTextbox = "#textboxSearch";
  TableDescription = "#divTableDescription";
  ColumnDescription = "#divColumnDescription";
  /* End Constants */
  window.setupPage = function() {
    var parameterContainer;
    foundContainer(jQuery('#divFoundContainer'), setTableDescription);
    tableDescriptionContainer(jQuery(TableDescription), setColumnDescription, sendTableDescriptionEdit);
    columnDescriptionContainer(jQuery(ColumnDescription), editColumnDescriptionMethod);
    parameterContainer = {
      errorContainer: ErrorContainer,
      searchForm: SearchForm,
      searchParameterName: SearchParameterName,
      searchTextbox: SearchTextbox,
      tableDescription: TableDescription
    };
    return runFormSetup(createObjectToSend, submitSearchFormResultBuilder(parameterContainer, hideFieldSets), parameterContainer);
  };
  /* Support Methods */
  hideFieldSets = function(caller) {
    if (caller = "search") {
      jQuery(TableDescription)[0].hideFieldSet();
      void 0;
    }
    jQuery(ColumnDescription)[0].hideFieldSet();
    return;
  };
  /* End Support Methods */
  /* Form Submital */
  runFormSetup = function(objectCreationMethod, resultMethod, parameterContainer) {
    var cleanTextboxSearch, validationMessages, validationRules;
    cleanTextboxSearch = clearHash(parameterContainer.searchTextbox);
    validationRules = new Object();
    validationRules[cleanTextboxSearch] = {
      required: true
    };
    validationMessages = new Object();
    validationMessages[cleanTextboxSearch] = {
      required: 'Search text is required.'
    };
    setupForm(validationRules, validationMessages, parameterContainer.searchForm, objectCreationMethod(parameterContainer.searchTextbox, parameterContainer.searchParameterName), parameterContainer.errorContainer, resultMethod);
    return;
  };
  createObjectToSend = function(searchTextbox, searchParameterName) {
    var finalObjectToSendMethod;
    return finalObjectToSendMethod = function() {
      var objectToSend;
      objectToSend = new Object();
      objectToSend[searchParameterName] = jQuery(searchTextbox).val();
      return objectToSend;
    };
  };
  submitSearchFormResultBuilder = function(parameterContainer, hideFieldsets) {
    var submitSearchFormResult;
    return submitSearchFormResult = function(result) {
      hideFieldsets("search");
      if (result.Success) {
        jQuery('#divFoundContainer')[0].updateResults(result);
        void 0;
      } else {
        updateErrorDivFromResult(result, clearHash(parameterContainer.searchTextbox), parameterContainer.searchForm, parameterContainer.errorContainer);
        void 0;
      }
      return;
    };
  };
  /* End Form Submital */
  /* Table Description */
  window.retrieveTableDescription = function(tableId, containerElement) {
    hideFieldSets("table");
    submitSimple({
      tableId: tableId
    }, "/Table/RetrieveTableInformationByTableId", createRetrieveTableDescriptionResultMethod(containerElement));
    return;
  };
  createRetrieveTableDescriptionResultMethod = function(containerElement) {
    var retrieveTableDescriptionResult;
    return retrieveTableDescriptionResult = function(result) {
      containerElement[0].updateDescription(result);
      return;
    };
  };
  sendTableDescriptionEdit = function(id, text, successMethod) {
    submitSimple({
      tableId: id,
      description: text
    }, "/Table/UpdateTableDescription", successMethod);
    return;
  };
  setTableDescription = function(id) {
    return retrieveTableDescription(id, jQuery(TableDescription));
  };
  /* End Table Description */
  /* Column Description */
  window.retrieveColumnDescription = function(columnId, containerElement) {
    return submitSimple({
      columnId: columnId
    }, "/Column/RetrieveColumnInformationByColumnId", createRetrieveColumnDescriptionResultMethod(containerElement));
  };
  window.setColumnDescription = function(id) {
    return retrieveColumnDescription(id, jQuery(ColumnDescription));
  };
  editColumnDescriptionMethod = function(id, text, successMethod) {
    submitSimple({
      columnId: id,
      description: text
    }, "/Column/UpdateColumnDescription", successMethod);
    return;
  };
  createRetrieveColumnDescriptionResultMethod = function(containerElement) {
    var retrieveTableDescriptionResult;
    return retrieveTableDescriptionResult = function(result) {
      containerElement[0].updateDescription(result);
      return;
    };
  };
  /* End Column Description */
}).call(this);
