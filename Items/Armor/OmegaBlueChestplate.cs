using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;

namespace CalamityModClassicPreTrailer.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class OmegaBlueChestplate : ModItem
	{
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Omega Blue Chestplate");
            /* Tooltip.SetDefault(@"12% increased damage and 8% increased critical strike chance
Your attacks inflict Crush Depth
No positive life regen"); */
		}

		public override void SetDefaults()
		{
            Item.width = 18;
            Item.height = 18;
			Item.value = Item.sellPrice(0, 38, 0, 0);
			Item.rare = 10;
			Item.defense = 28;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}

        public override void UpdateEquip(Player player)
        {
            const float damageUp = 0.12f;
            const int critUp = 8;
            player.GetDamage(DamageClass.Melee) += damageUp;
            player.GetDamage(DamageClass.Ranged) += damageUp;
            player.GetDamage(DamageClass.Magic) += damageUp;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage += damageUp;
            player.GetDamage(DamageClass.Summon) += damageUp;
            player.GetCritChance(DamageClass.Melee) += critUp;
            player.GetCritChance(DamageClass.Ranged) += critUp;
            player.GetCritChance(DamageClass.Magic) += critUp;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingCrit += critUp;

            //insert enable crush depth, disable positive life regen
            player.GetModPlayer<CalamityPlayerPreTrailer>().omegaBlueChestplate = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("ReaperTooth").Type, 16);
            recipe.AddIngredient(Mod.Find<ModItem>("Lumenite").Type, 8);
            recipe.AddIngredient(Mod.Find<ModItem>("Tenebris").Type, 8);
            recipe.AddIngredient(Mod.Find<ModItem>("RuinousSoul").Type, 4);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
