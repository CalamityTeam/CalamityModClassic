using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;

namespace CalamityModClassicPreTrailer.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class OmegaBlueHelmet : ModItem
	{
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Omega Blue Helmet");
            /* Tooltip.SetDefault(@"You can move freely through liquids
12% increased damage and 8% increased critical strike chance
+2 max minions"); */
		}

		public override void SetDefaults()
		{
            Item.width = 18;
            Item.height = 18;
			Item.value = Item.sellPrice(0, 35, 0, 0);
			Item.rare = 10;
			Item.defense = 19;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}

        public override void UpdateEquip(Player player)
        {
            player.ignoreWater = true;

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

            player.maxMinions += 2;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("OmegaBlueChestplate").Type && legs.type == Mod.Find<ModItem>("OmegaBlueLeggings").Type;
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawOutlines = true;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = @"Increases armor penetration by 50
10% increased damage and critical strike chance
Short-ranged tentacles heal you by sucking enemy life
Press Y to activate abyssal madness for 5 seconds
Abyssal madness increases damage, critical strike chance, and tentacle aggression/range
This effect has a 30 second cooldown";

            player.GetArmorPenetration(DamageClass.Generic) += 50;

            //raise rev caps
            player.GetModPlayer<CalamityPlayerPreTrailer>().omegaBlueSet = true;

            if (player.GetModPlayer<CalamityPlayerPreTrailer>().omegaBlueCooldown > 0)
            {
                if (player.GetModPlayer<CalamityPlayerPreTrailer>().omegaBlueCooldown == 1) //dust when ready to use again
                {
                    for (int i = 0; i < 66; i++)
                    {
                        int d = Dust.NewDust(player.position, player.width, player.height, 20, 0, 0, 100, Color.Transparent, 2.6f);
                        Main.dust[d].noGravity = true;
                        Main.dust[d].noLight = true;
                        Main.dust[d].fadeIn = 1f;
                        Main.dust[d].velocity *= 6.6f;
                    }
                }
                player.GetModPlayer<CalamityPlayerPreTrailer>().omegaBlueCooldown--;
            }

            if (player.GetModPlayer<CalamityPlayerPreTrailer>().omegaBlueCooldown > 1500)
            {
                player.GetModPlayer<CalamityPlayerPreTrailer>().omegaBlueHentai = true;

                int d = Dust.NewDust(player.position, player.width, player.height, 20, 0, 0, 100, Color.Transparent, 1.6f);
                Main.dust[d].noGravity = true;
                Main.dust[d].noLight = true;
                Main.dust[d].fadeIn = 1f;
                Main.dust[d].velocity *= 3f;
            }
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("ReaperTooth").Type, 11);
            recipe.AddIngredient(Mod.Find<ModItem>("Lumenite").Type, 5);
            recipe.AddIngredient(Mod.Find<ModItem>("Tenebris").Type, 5);
            recipe.AddIngredient(Mod.Find<ModItem>("RuinousSoul").Type, 2);
            recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}
}
