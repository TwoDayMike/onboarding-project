import FormTodo from "components/FormTodo";
import TodoTable from "components/TodoTable";
import { Locale } from "i18n/Locale";
import { GetStaticProps, NextPage } from "next";
import { I18nProps } from "next-rosetta";
import { genApiClient } from "services/backend/genApiClient";
import { useLocale } from "services/locale/useLocale";
import { useMemoAsync } from "utils/hooks/useMemoAsync";

const Page: NextPage = () => {
  const { t } = useLocale();


  return (
    <>
    <FormTodo/>
    <TodoTable/>
    </>
    
  );
};

// export const getStaticProps: GetStaticProps<I18nProps<Locale>> = async (
//   context
// ) => {
  

//   return {
    
//   };
// };

Page.displayName = "Page";

export default Page;
