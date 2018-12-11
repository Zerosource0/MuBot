import axios from "axios";
import { Md5 } from "ts-md5/dist/md5";
import { loginRequest, createUserRequest } from "requests";

const instance = axios.create({
    baseURL: (process.env.VUE_APP_APPLICATION_URL as string) + "/api/login",
    headers: {
        "Content-Type": "application/json",
    }
});

async function verifyToken(): Promise<boolean> {
    const token: string = localStorage.getItem("mubotToken") as string;
    if (!token) {
        return false;
    }

    const newInstance = instance;
    newInstance.defaults.headers.common["Authorization"] = "bearer " + token;
    let result: boolean = false;
    await instance.get("verify")
        .then((response) => {
            if (response.status === 200) {
                result = true;
            } else {
                localStorage.removeItem("mubotToken");
            }
        });
    return result;
}

async function login(username: string, password: string): Promise<string> {
    if (!username || !password) { return "failed"; }

    const passwordHash: string = Md5.hashStr(password).toString();
    const request: loginRequest = { username, password: passwordHash };
    let result: string = "";

    await instance.post("/authenticate", request)
        .then((response) => {
            localStorage.setItem("mubotToken", response.data);
            result = "success";
        })
        .catch((error) => {
            result = error.response.data.error;
        });
    return result;
}

async function createUser(username: string, password: string, email: string, firstName?: string, lastName?: string) {
    if (!username || !password || !email) { return; }

    const passwordHash: string = Md5.hashStr(password).toString();
    const request: createUserRequest = { username, password: passwordHash, email: "", firstName: "", lastName: "" };

    await instance.post("/create", request)
        .then((response) => {
            console.log(response);
        })
        .catch((error) => {
            console.log(error);
        });
}

async function ping(): Promise<boolean> {
    let result: boolean = false;
    await instance.get("")
        .then(response => {
            if (response.status === 200) {
                result = true;
            }
        }).catch(error => {
            result = false;
        });
    return result;
}

export default {
    login,
    createUser,
    ping,
    verifyToken
};
