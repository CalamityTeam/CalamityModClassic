using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Armor
{
    public class SirensHeartAlt : ModItem
    {
        public override void Load()
        {
            // this code wasn't here originally but it's here out of necessity, what with having to load these assets without an implicit path to them
            if (Main.netMode != NetmodeID.Server)
            {
                EquipLoader.AddEquipTexture(Mod, "CalamityModClassicPreTrailer/Items/Armor/SirenTransAlt_Head", EquipType.Head, this);
                EquipLoader.AddEquipTexture(Mod, "CalamityModClassicPreTrailer/Items/Armor/SirenTransAlt_Body", EquipType.Body, this);
                EquipLoader.AddEquipTexture(Mod, "CalamityModClassicPreTrailer/Items/Armor/SirenTransAlt_Legs", EquipType.Legs, this);
            }
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Siren's Heart (Green)");
            /* Tooltip.SetDefault("Transforms the holder into a siren\n" +
                "Siren scales give increased defense (gives more defense in hardmode and post-ML)\n" +
                "Siren sight reveals danger locations (green-only)\n" +
                "Increases life regen (gives more life regen in hardmode and post-ML)\n" +
                "Going underwater gives you a buff\n" +
                "Greatly reduces breath loss in the abyss\n" +
                "Enemies become frozen when they touch you\n" +
                "You have a layer of ice around you that absorbs 15% damage but breaks after one hit\n" +
                "After 30 seconds the ice shield will regenerate\n" +
                "Your alluring figure allows you to buy items at a reduced price from town npcs (only works in hardmode)\n" +
                "Wow, you can swim now!\n" +
                "Most of these effects are only active after Skeletron has been defeated\n" +
                "Revengeance drop"); */
            if (Main.netMode != NetmodeID.Server)
            {
                int sirenHeadAlt = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Head);
                int sirenBodyAlt = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Body);
                int sirenLegsAlt = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Legs);
                ArmorIDs.Head.Sets.DrawHead[sirenHeadAlt] = false;
                ArmorIDs.Body.Sets.HidesTopSkin[sirenBodyAlt] = true;
                ArmorIDs.Body.Sets.HidesArms[sirenBodyAlt] = true;
                ArmorIDs.Legs.Sets.HidesBottomSkin[sirenLegsAlt] = true;
                //ArmorIDs.Shoe.Sets.OverridesLegs[sirenLegsAlt] = true;
            }
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.accessory = true;
            Item.value = Item.buyPrice(0, 45, 0, 0);
            Item.rare = 7;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.sirenBoobsAlt = true;
            if (hideVisual)
            {
                modPlayer.sirenBoobsAltHide = true;
            }
        }
    }
}