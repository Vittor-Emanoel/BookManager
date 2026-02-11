import { Header } from "@/view/components/header";
import { SearchBar } from "@/view/components/search-bar";

export const Home = () => {
  return (
    <div className="px-16 py-8">
      <Header />
      <section className="mt-8">
        <SearchBar />
      </section>
    </div>
  );
};
