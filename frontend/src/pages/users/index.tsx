import UserTable from "components/UserTable";
import useUsers from "hooks/useGetUsers";
import { useToken } from "hooks/useToken";
import { NextPage } from "next";
import { useRouter } from "next/router";
import { useEffect } from "react";

const Page: NextPage = () => {
    const { hasToken } = useToken();
    const router = useRouter();
    const { data } = useUsers();
    console.log(data)

    useEffect(() => {
        if (!hasToken)
            router.push("/login")
    }, [])

    return (<div><UserTable /></div>)
}

export default Page;