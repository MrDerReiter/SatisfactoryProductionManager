import ImageEntry from "./ImageEntry";


interface ProductEntryProps {
  product: [string, number];
  size: number;
  callback: (value: number) => string | null;
}

export default function ProductEntry(props: ProductEntryProps) {
  const { product, size, callback } = props;
  return (
    <div className="double-bordered" style={{ marginRight: 5 }}>
      <ImageEntry {...{ size, callback }}
        content={[`images/Resources/${product[0]}.png`, product[1]]}
        colors={["lightblue", "cadetblue"]}
        key={product.join(" ")} />
    </div>
  );
}