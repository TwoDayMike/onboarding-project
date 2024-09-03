import {getCookie, deleteCookie} from 'cookies-next'
interface UserObject{
    id: number
}
const resolveCookie = () => {
    const token = getCookie("accessToken");
    console.log(token)
    const hasToken = token?.toString().split(" ")[1] !== undefined;

    const jwtPayload = token?.toString().split(".")[1].trim();
    console.log(jwtPayload)

    const userObject = hasToken ? JSON.parse(atob(jwtPayload)) : null;



    function logout() {
        console.log("Signing out user.")
        deleteCookie("accessToken")
    }   

    return {
        token,
        hasToken,
        logout,
        userObject
    }

}

export function useToken() {
    return resolveCookie();
}