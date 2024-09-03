import RegisterForm from "components/RegisterForm";
import Form from "components/UI/Form";
import { NextPage } from "next";

const Register: NextPage = () => {
    return (
        <div className="flex justify-center items-center h-screen">
            <RegisterForm />
        </div>
    )

}

export default Register;