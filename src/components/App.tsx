import HeaderPanel from "./panels/HeaderPanel";
import LeftPanel from "./panels/LeftPanel";
import Workspace from "./panels/Workspace";
import LineWorkspace from "./panels/LineWorkspace";
import BlockWorkspace from "./panels/BlockWorkspace";
import RecipeSelector from "./dialogs/RecipeSelector";
import ErrorMessage from "./dialogs/ErrorMessage";
//Unified .css entry point
import "../style/.index";


export default function App() {
  return (
    <>
      {/*General interface*/}
      <HeaderPanel />
      <LeftPanel />
      <Workspace>
        <LineWorkspace />
        <BlockWorkspace />
      </Workspace>

      {/*Modal windows*/}
      <RecipeSelector />
      <ErrorMessage />
    </>
  );
}