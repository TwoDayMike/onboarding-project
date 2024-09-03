import LoginForm from "components/LoginForm";
import { NextPage } from "next"

const Page: NextPage = () => {


    return (
        <div className="flex justify-center items-center h-screen">
            <LoginForm />
        </div>
    )
}

export default Page;