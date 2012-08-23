### Begin Constants ###

ErrorContainer = "#divError"
SearchForm = "#formSearchText"
SearchParameterName = "partialName"
SearchTextbox = "#textboxSearch"

TableDescription = "#divTableDescription"
ColumnDescription = "#divColumnDescription"
	
### End Constants ###


window.setupPage = () ->

  foundContainer jQuery('#divFoundContainer'), setTableDescription
  tableDescriptionContainer jQuery(TableDescription), setColumnDescription, sendTableDescriptionEdit
  columnDescriptionContainer jQuery(ColumnDescription), editColumnDescriptionMethod

  parameterContainer =
    errorContainer: ErrorContainer
    searchForm: SearchForm
    searchParameterName: SearchParameterName
    searchTextbox: SearchTextbox
    tableDescription: TableDescription

  runFormSetup(createObjectToSend, submitSearchFormResultBuilder(parameterContainer, hideFieldSets), parameterContainer)


### Support Methods ###


hideFieldSets = (caller) ->
  if caller = "search"
    jQuery(TableDescription)[0].hideFieldSet()
    undefined

  jQuery(ColumnDescription)[0].hideFieldSet()
  undefined

### End Support Methods ###


### Form Submital ###

runFormSetup = (objectCreationMethod, resultMethod, parameterContainer) ->
  cleanTextboxSearch = clearHash(parameterContainer.searchTextbox)

  validationRules = new Object()
  validationRules[cleanTextboxSearch] = { required: true }

  validationMessages = new Object()
  validationMessages[cleanTextboxSearch] = { required: 'Search text is required.' }

  setupForm(validationRules, validationMessages, parameterContainer.searchForm, objectCreationMethod(parameterContainer.searchTextbox, parameterContainer.searchParameterName), parameterContainer.errorContainer, resultMethod)
  undefined

createObjectToSend = (searchTextbox, searchParameterName) ->
  finalObjectToSendMethod  = () ->
    objectToSend = new Object()
    objectToSend[searchParameterName] = jQuery(searchTextbox).val()
    objectToSend

submitSearchFormResultBuilder = (parameterContainer, hideFieldsets) ->
  submitSearchFormResult = (result) ->
    hideFieldsets("search")
    if result.Success
        jQuery('#divFoundContainer')[0].updateResults result
        undefined
      else
        updateErrorDivFromResult(result, clearHash(parameterContainer.searchTextbox), parameterContainer.searchForm, parameterContainer.errorContainer)
        undefined

    undefined

### End Form Submital ###

### Table Description ###

window.retrieveTableDescription = (tableId, containerElement) ->
  hideFieldSets("table")
  submitSimple({tableId: tableId}, "/Table/RetrieveTableInformationByTableId", createRetrieveTableDescriptionResultMethod(containerElement))
  undefined


createRetrieveTableDescriptionResultMethod  = (containerElement) ->
  retrieveTableDescriptionResult = (result) ->
    containerElement[0].updateDescription(result)
    undefined

sendTableDescriptionEdit = (id, text, successMethod) ->
  submitSimple {tableId: id, description: text}, "/Table/UpdateTableDescription", successMethod
  undefined

setTableDescription = (id) ->
  retrieveTableDescription(id, jQuery(TableDescription))

### End Table Description ###

### Column Description ###

window.retrieveColumnDescription = (columnId, containerElement) ->
  submitSimple({columnId: columnId}, "/Column/RetrieveColumnInformationByColumnId", createRetrieveColumnDescriptionResultMethod(containerElement))

window.setColumnDescription = (id) ->
  retrieveColumnDescription id, jQuery(ColumnDescription)


editColumnDescriptionMethod = (id, text, successMethod) ->
  submitSimple {columnId: id, description: text}, "/Column/UpdateColumnDescription", successMethod
  undefined

createRetrieveColumnDescriptionResultMethod  = (containerElement) ->
  retrieveTableDescriptionResult = (result) ->
    containerElement[0].updateDescription(result)
    undefined


### End Column Description ###



