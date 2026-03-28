using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Permafrost
{
    public class Popo : ModItem
    {
        public override void Load()
        {
            // this code wasn't here originally but it's here out of necessity, what with having to load these assets without an implicit path to them
            if (Main.netMode != NetmodeID.Server)
            {
                EquipLoader.AddEquipTexture(Mod, "CalamityModClassicPreTrailer/Items/Permafrost/Popo_Head", EquipType.Head, this);
                EquipLoader.AddEquipTexture(Mod, "CalamityModClassicPreTrailer/Items/Permafrost/PopoNoseless_Head", EquipType.Head, name: "PopoNoseless");
                EquipLoader.AddEquipTexture(Mod, "CalamityModClassicPreTrailer/Items/Permafrost/Popo_Body", EquipType.Body, this);
                EquipLoader.AddEquipTexture(Mod, "CalamityModClassicPreTrailer/Items/Permafrost/Popo_Legs", EquipType.Legs, this);
            }
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Magic Scarf and Hat");
            /* Tooltip.SetDefault("Transforms the holder into a snowman\n" +
                "Don't let the demons steal your nose"); */
            if (Main.netMode == NetmodeID.Server)
                return;
            
            int equipSlotBody = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Body);
            ArmorIDs.Body.Sets.HidesTopSkin[equipSlotBody] = true;
            ArmorIDs.Body.Sets.HidesArms[equipSlotBody] = true;

            int equipSlotLegs = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Legs);
            ArmorIDs.Legs.Sets.HidesBottomSkin[equipSlotLegs] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 30;
            Item.accessory = true;
            Item.value = 1000000;
            Item.rare = 5;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.snowman = true;
            if (hideVisual)
            {
                modPlayer.snowmanHide = true;
            }
        }
    }
}