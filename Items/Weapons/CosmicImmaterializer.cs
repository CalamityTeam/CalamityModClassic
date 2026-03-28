using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.IO;
using Terraria.ObjectData;
using Terraria.Utilities;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
    public class CosmicImmaterializer : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cosmic Immaterializer");
            /* Tooltip.SetDefault("Summons a cosmic energy spiral to fight for you\n" +
                               "The orb will fire swarms of homing energy bolts when enemies are detected by it\n" +
                               "Requires 10 minion slots to use\n" +
                               "There can only be one\n" +
                               "Without a summoner armor set bonus this minion will deal less damage"); */
        }

        public override void SetDefaults()
        {
            Item.mana = 100;
            Item.damage = 3000;
            Item.useStyle = 1;
            Item.width = 74;
            Item.height = 72;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.noMelee = true;
            Item.knockBack = 0f;
            Item.value = Item.buyPrice(2, 50, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item60;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("CosmicEnergy").Type;
            Item.shootSpeed = 10f;
            Item.DamageType = DamageClass.Summon;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 15;
		}

        public override bool CanUseItem(Player player)
        {
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile p = Main.projectile[i];
                if (p.active && p.type == Mod.Find<ModProjectile>("CosmicEnergy").Type && p.owner == player.whoAmI)
                {
                    return false;
                }
            }
            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			bool hasSummonerSet = modPlayer.tarraSummon || modPlayer.bloodflareSummon || modPlayer.godSlayerSummon || modPlayer.silvaSummon;
			player.itemTime = Item.useTime;
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float num78 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
            float num79 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
            if (player.gravDir == -1f)
            {
                num79 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector2.Y;
            }
            float num80 = (float)Math.Sqrt((double)(num78 * num78 + num79 * num79));
            float num81 = num80;
            if ((float.IsNaN(num78) && float.IsNaN(num79)) || (num78 == 0f && num79 == 0f))
            {
                num78 = (float)player.direction;
                num79 = 0f;
                num80 = Item.shootSpeed;
            }
            else
            {
                num80 = Item.shootSpeed / num80;
            }
            num78 = 0f;
            num79 = 0f;
            vector2.X = (float)Main.mouseX + Main.screenPosition.X;
            vector2.Y = (float)Main.mouseY + Main.screenPosition.Y;
            Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, num78, num79, type, (int)((double)damage * (hasSummonerSet ? 1.0 : 0.66)), knockback, player.whoAmI, 0f, 0f);
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "SunGodStaff");
            recipe.AddIngredient(null, "AncientIceChunk");
            recipe.AddIngredient(null, "ElementalAxe");
            recipe.AddIngredient(null, "EnergyStaff");
            recipe.AddIngredient(null, "NightmareFuel", 5);
            recipe.AddIngredient(null, "EndothermicEnergy", 5);
            recipe.AddIngredient(null, "CosmiliteBar", 5);
            recipe.AddIngredient(null, "DarksunFragment", 5);
            recipe.AddIngredient(null, "HellcasterFragment", 3);
            recipe.AddIngredient(null, "Phantoplasm", 5);
            recipe.AddIngredient(null, "AuricOre", 25);
            recipe.AddTile(null, "DraedonsForge");
            recipe.Register();
        }
    }
}