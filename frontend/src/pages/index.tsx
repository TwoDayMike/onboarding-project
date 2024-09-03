import TodoTable from "components/TodoTable";
import useLogin, { LoginRequest } from "hooks/useLogin";
import { useToken } from "hooks/useToken";
import { Locale } from "i18n/Locale";
import { GetStaticProps, NextPage } from "next";
import { I18nProps } from "next-rosetta";
import { useRouter } from "next/router";
import { SubmitHandler, useForm } from "react-hook-form";
import { genApiClient } from "services/backend/genApiClient";
import { useLocale } from "services/locale/useLocale";
import { useMemoAsync } from "utils/hooks/useMemoAsync";

const Page: NextPage = () => {
  const { t } = useLocale();
  const {mutateAsync, isSuccess} = useLogin();
  const {register, handleSubmit} = useForm();
  const router = useRouter()
  const onSubmit : SubmitHandler<LoginRequest> = async (data, e ) => {
    e.preventDefault()
    await mutateAsync(data).then((e) => {
      router.push("/me")
    });
  }

  //const {token } = useToken()
  const { value, loading } = useMemoAsync(
    async () => {
      const client = await genApiClient();
      const result = await client.templateExampleCustomer_GetAll();
      const test = await client.index_Hello("bob");
      return result;
    },
    [],
    []
  );

  return (
    <>
      <h1>{t("strings.example")}</h1>
      <TodoTable/>
      {loading && <div>... loading</div>}
      {!loading && value && <div>{JSON.stringify(value, null, 2)}</div>}
      <form className="flex flex-col w-1/3 justify-center items-center" onSubmit={handleSubmit(onSubmit)}>
        <input className="border" {...register("email")}></input>
        <input className="border" {...register("password")}></input>
        <button className="border" type="submit">test</button>
      </form>
    </>
  );
};

export const getStaticProps: GetStaticProps<I18nProps<Locale>> = async (
  context
) => {
  const locale = context.locale || context.defaultLocale;
  const { table = {} } = await import(`../i18n/${locale}`); //!Note you might need to change the path depending on page depth

  return {
    props: { table },
  };
};

Page.displayName = "Page";

export default Page;
