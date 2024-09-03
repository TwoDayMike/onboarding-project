import FormTodo from "components/FormTodo";
import TodoTable from "components/TodoTable";
import { Locale } from "i18n/Locale";
import { GetStaticProps, NextPage } from "next";
import { I18nProps } from "next-rosetta";
import { genApiClient } from "services/backend/genApiClient";
import { useLocale } from "services/locale/useLocale";
import { useMemoAsync } from "utils/hooks/useMemoAsync";
import { getCookie } from "cookies-next";
import { useRouter } from "next/router";
import { useEffect, useState } from "react";
import { useToken } from "hooks/useToken";
import AuthTodoTable from "components/AuthTodoTable";
import FilterTodoTable from "components/FilterTodoTable";


const Page: NextPage = () => {
  const { t } = useLocale();
  const router = useRouter();
  const { token, hasToken, logout } = useToken()
  const [currentToken, setCurrentToken] = useState<string | undefined>(undefined);
  const handleSignOut = () => {
    logout()
    router.push("/login")
  }

  useEffect(() => {
    if (!hasToken) {
      router.replace("/")
      return;
    }

    setCurrentToken(token.toString())
  }, [])

  return (
    <>
      <div className="border-b-2 p-2">
        {/* <AuthTodoTable token={currentToken} /> */}
      </div>
      <div className="border-b-2 p-2">
      <FilterTodoTable token={token?.toString()}/>
      </div>
      <button onClick={() => handleSignOut()}></button>
    </>

  );
};

export const getStaticProps: GetStaticProps = async (
  context
) => {


  return {
    props: {  },
  };
};

Page.displayName = "Page";

export default Page;
