import axios from "axios";

axios.defaults.headers.post["Content-Type"] = "application/x-www-form-urlencoded";
axios.defaults.transformRequest = [(data, headers) => {
    const str: any[] = [];
    for (const p in data) {
        if (data.hasOwnProperty(p) && data[p]) {
            str.push(encodeURI(p) + "=" + encodeURI(data[p]));
        }
    }
    return str.join("&");
}];

axios.interceptors.response.use((response) => {
    // Do something with response data
    return response;
}, (error) => {
    // Do something with response error
    return Promise.reject(error);
});
