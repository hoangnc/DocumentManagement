function typeOfVar(obj) {
    return {}.toString.call(obj).split(' ')[1].slice(0, -1).toLowerCase();
}

function isVariableHaveDefaultVal(variable) {
    if (typeof (variable) === 'string') {  // number, boolean, string, object
        return (variable.trim().length === 0) ? true : false;
    } else if (typeof (variable) === 'boolean') {
        return (variable === false) ? true : false;
    } else if (typeof (variable) === 'undefined') {
    } else if (typeof (variable) === 'number') {
        return (variable === 0) ? true : false;
    } else if (typeof (variable) === 'object') {
        //   -----Object-----
        if (typeOfVar(variable) === 'array' && variable.length === 0) {
            return true;
        } else if (typeOfVar(variable) === 'string' && variable.length === 0) {
            return true;
        } else if (typeOfVar(variable) === 'boolean') {
            return (variable === false) ? true : false;
        } else if (typeOfVar(variable) === 'number') {
            return (variable === 0) ? true : false;
        } else if (typeOfVar(variable) === 'regexp' && variable.source.trim().length === 0) {
            return true;
        } else if (variable === null) {
            return true;
        }
    }
    return false;
}

var groupsrenderer = function (text, group, expanded, data) {
    var number = dataAdapter.formatNumber(group, data.groupcolumn.cellsformat);
    var text = data.groupcolumn.text + ': ' + number;
    var aggregate = this.getcolumnaggregateddata('price', ['sum'], true, data.subItems);
    return '<div class="' + toThemeProperty('jqx-grid-groups-row') + '" style="position: absolute;"><span>' + text + ', </span>' + '<span class="' + toThemeProperty('jqx-grid-groups-row-details') + '">' + "Total price" + ' (' + aggregate.sum + ')' + '</span></div>';
}