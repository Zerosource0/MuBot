import axios, { AxiosInstance, AxiosResponse } from "axios";
import HttpStatus from "http-status-codes";
import qs from "qs";

export default {
    get,
    post,
    put,
    deleteReq
};

interface IClientResult<T> {
    model: T;
}

async function get<T>(url: string, params?: any, axoiosInstance?: AxiosInstance): Promise<T> {
    return getAxios(axoiosInstance)
        .get(
            getUrl(url),
            params
                ? {
                    params,
                    paramsSerializer: p => {
                        return qs.stringify(p, { arrayFormat: "repeat" });
                    }
                }
                : undefined
        )
        .then(response => {
            if (response.status === HttpStatus.OK) {
                return response.data as T;
            } else {
                handleNotOk(response);
                throw new Error(response.statusText);
            }
        })
        .catch(response => {
            throw new Error(response);
        });
}

async function post<T>(url: string, payload?: any, axoiosInstance?: AxiosInstance): Promise<T> {
    return getAxios(axoiosInstance)
        .post(getUrl(url), payload, {
            validateStatus: (status: any) => {
                return status < 500; // reject only if the status code is greater than or equal to 500
            }
        })
        .then(response => {
            if (response.status === HttpStatus.OK) {
                return response.data as T;
            } else if (response.status === HttpStatus.NO_CONTENT) {
                // for T = void situation
                return {} as T;
            } else if (response.status === HttpStatus.BAD_REQUEST) {
                // $serverValidationError(response.data);
                throw new Error(response.statusText);
            } else {
                handleNotOk(response);
                throw new Error(response.statusText);
            }
        })
        .catch(response => {
            throw new Error(response);
        });
}

async function put<T>(url: string, payload?: any, axoiosInstance?: AxiosInstance): Promise<T> {
    return getAxios(axoiosInstance)
        .put(getUrl(url), payload, {
            validateStatus: (status: any) => {
                return status < 500; // reject only if the status code is greater than or equal to 500
            }
        })
        .then(response => {
            if (response.status === HttpStatus.OK) {
                return response.data as T;
            } else if (response.status === HttpStatus.NO_CONTENT) {
                // for T = void situation
                return {} as T;
            } else if (response.status === HttpStatus.BAD_REQUEST) {
                // EventBus.serverValidationError(response.data);
                throw new Error(response.statusText);
            } else {
                handleNotOk(response);
                throw new Error(response.statusText);
            }
        })
        .catch(response => {
            throw new Error(response);
        });
}

async function deleteReq<T>(url: string, params?: any, axoiosInstance?: AxiosInstance): Promise<T> {
    return getAxios(axoiosInstance)
        .delete(
            getUrl(url),
            params
                ? {
                    params,
                    paramsSerializer: p => {
                        return qs.stringify(p, { arrayFormat: "repeat" });
                    }
                }
                : undefined
        )
        .then(response => {
            if (response.status === HttpStatus.OK) {
                return response.data;
            } else {
                handleNotOk(response);
                throw new Error(response.statusText);
            }
        })
        .catch(response => {
            throw new Error(response);
        });
}


function handleNotOk(response: AxiosResponse): void {
    // todo handle statuses
    switch (response.status) {
        case 304:
            break;
        default:
            break;
    }
}

function getAxios(axoiosInstance?: AxiosInstance): AxiosInstance {

    if (axoiosInstance) {
        return axoiosInstance;
    }

    return axios.create({
        headers: {
        }
    });
}

function getUrl(url: string, isRelative?: boolean): string {
    if (isRelative) {
        return `/api/${url}`;
    }
    return url;
}
