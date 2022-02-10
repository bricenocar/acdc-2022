import { FC } from 'react';
import { DetailsList, DetailsListLayoutMode, Selection, IColumn, SelectionMode } from '@fluentui/react/lib/DetailsList';
import { Stack } from '@fluentui/react';

export interface CommonDetailsListItem {
  key: number;
  name: string;
  value: string;
}

export interface CommonDetailsListProps {
  columns: IColumn[]
  hidden: boolean;
  items: CommonDetailsListItem[];
  itemInvoked?: (item: CommonDetailsListItem) => void;
  itemSelected?: (item: CommonDetailsListItem) => void;
}

export const CommonDetailsList: FC<CommonDetailsListProps> = ({ columns, items, itemInvoked, itemSelected, hidden }) => {

  const onItemInvoked = (item: CommonDetailsListItem): void => {
    if (itemInvoked) {
      itemInvoked(item);
    }
  };

  const onItemSelectem = (): void => {
    if (itemSelected) {
      const item = selection.getSelection()[0] as CommonDetailsListItem;
      itemSelected(item);
    }
  };

  const selection = new Selection({
    onSelectionChanged: () => onItemSelectem(),
  });

  return (
    <Stack>
      {!hidden &&
        <DetailsList
          items={items}
          columns={columns}
          selection={selection}
          setKey="set"
          layoutMode={DetailsListLayoutMode.justified}
          selectionPreservedOnEmptyClick={true}
          ariaLabelForSelectionColumn="Toggle selection"
          ariaLabelForSelectAllCheckbox="Toggle selection for all items"
          checkButtonAriaLabel="select row"
          onItemInvoked={onItemInvoked}
          selectionMode={SelectionMode.single}
          isHeaderVisible={false}
        />
      }
    </Stack>
  );
}
