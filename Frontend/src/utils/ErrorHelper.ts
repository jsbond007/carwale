
export function StringifyErrors(httpResponse: any) {
    let allErrors: string = "";
    if (httpResponse?.data?.errors) {
        Object.keys(httpResponse?.data?.errors).forEach((field) => {
            let thisError: string = "";
            Object.keys(httpResponse?.data?.errors[field]).forEach((key) => {
                thisError += `<br/>${httpResponse?.data?.errors[field][key]}`
            });
            allErrors += `<br/>${thisError}`;
        });        
    }

    return `<div>${allErrors}</div>`;
}