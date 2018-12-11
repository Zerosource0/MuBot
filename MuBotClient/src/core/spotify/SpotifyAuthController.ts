import { Md5 } from "ts-md5/dist/md5";
import axios from "axios";
import { createTokenRequest } from "requests";

const clientId = process.env.VUE_APP_CLIENT_ID as string;
const clientSecret = process.env.VUE_APP_CLIENT_SECRET as string;
const clientUrl = process.env.VUE_APP_CLIENT_URL as string;
const redirectUri = clientUrl + "/callback";

const instance = axios.create({
    baseURL: (process.env.VUE_APP_APPLICATION_URL as string) + "/api/spotify/auth",
    headers: {
        "Content-Type": "application/json",
        "Authorization": "bearer " + localStorage.getItem("mubotToken")
    }
});

export const ProxyLink = "https://cors-anywhere.herokuapp.com/";

function Authorize() {
    const url = ProxyLink + "https://accounts.spotify.com/authorize";
    let qs = "?client_id=" + clientId;
    qs += "&response_type=code";
    qs += "&redirect_uri=" + encodeURIComponent(redirectUri);
    qs += "&state=" + Md5.hashStr(process.env.VUE_APP_CLIENT_HASH as string).toString();
    qs += "&scope=playlist-modify-public";

    const authInstance = axios.create({
        baseURL: url + qs,
        headers: {
            "Accept": "*",
            "Content-Type": "application/json",
            "Access-Control-Allow-Origin": "*",
            "X-Requested-With": "xmlhttprequest",
            "Access-Control-Request-Origin": "*",
            // tslint:disable-next-line:max-line-length
            "Authorization": "Basic " + btoa(clientId + ":" + clientSecret)
        }
    });

    authInstance.get("").then((response) => {
        window.location.href = response.headers["x-final-url"];
    });
    return;
}

async function HasToken(): Promise<boolean> {
    let result: boolean = false;
    await instance.get("")
        .then((response) => {
            if (response.status === 200) {
                result = true;
            }
        });
    return result;
}

async function CreateToken(code: string, state?: string): Promise<boolean> {
    let result: boolean = false;
    const request: createTokenRequest = { code, redirectUri };
    await instance.post("", request)
        .then((response) => {
            console.log(response);
            result = true;
        })
        .catch((error) => {
            console.log(error);
        });
    return result;
}

export default {
    Authorize,
    HasToken,
    CreateToken
};
