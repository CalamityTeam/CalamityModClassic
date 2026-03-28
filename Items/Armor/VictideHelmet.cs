using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items.Armor;

namespace CalamityModClassicPreTrailer.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class VictideHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Victide Helmet");
            /* Tooltip.SetDefault("9% increased minion damage\n" +
                "+1 max minion"); */
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
			Item.value = Item.buyPrice(0, 1, 50, 0);
			Item.rare = 2;
            Item.defense = 1; //8
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("VictideBreastplate").Type && legs.type == Mod.Find<ModItem>("VictideLeggings").Type;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Increased life regen and minion damage while submerged in liquid\n" +
                "Summons a sea urchin to protect you\n" +
                "When using any weapon you have a 10% chance to throw a returning seashell projectile\n" +
                "This seashell does true damage and does not benefit from any damage class\n" +
                "Slightly reduces breath loss in the abyss";
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.victideSet = true;
            modPlayer.urchin = true;
            if (player.whoAmI == Main.myPlayer)
            {
                if (player.FindBuffIndex(Mod.Find<ModBuff>("Urchin").Type) == -1)
                {
                    player.AddBuff(Mod.Find<ModBuff>("Urchin").Type, 3600, true);
                }
                if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("Urchin").Type] < 1)
                {
                    Projectile.NewProjectile(Entity.GetSource_FromThis(null), player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("Urchin").Type, (int)(7f * player.GetDamage(DamageClass.Summon).Multiplicative), 0f, Main.myPlayer, 0f, 0f);
                }
            }
            player.ignoreWater = true;
            if (Collision.DrownCollision(player.position, player.width, player.height, player.gravDir))
            {
                player.GetDamage(DamageClass.Summon) += 0.1f;
                player.lifeRegen += 3;
            }
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.09f;
            player.maxMinions++;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "VictideBar", 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}