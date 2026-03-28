using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;
using CalamityModClassicPreTrailer.Projectiles;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
    public class PlagueKeeper : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Plague Keeper");
        }

        public override void SetDefaults()
        {
            Item.width = 74;
            Item.damage = 110;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.useAnimation = 18;
            Item.useStyle = 1;
            Item.useTime = 18;
            Item.useTurn = true;
            Item.knockBack = 6f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.height = 92;
            Item.value = Item.buyPrice(1, 20, 0, 0);
            Item.rare = 10;
            Item.shoot = Mod.Find<ModProjectile>("PlagueBeeDust").Type;
            Item.shootSpeed = 9f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "VirulentKatana");
            recipe.AddIngredient(ItemID.BeeKeeper);
            recipe.AddIngredient(ItemID.FragmentSolar, 10);
            recipe.AddIngredient(ItemID.LunarBar, 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 300);
            for (int i = 0; i < 3; i++)
            {
                int bee = Projectile.NewProjectile(Entity.GetSource_FromThis(null), player.Center.X, player.Center.Y, 0f, 0f, player.beeType(), 
                    player.beeDamage(Item.damage / 3), player.beeKB(0f), player.whoAmI, 0f, 0f);
                Main.projectile[bee].penetrate = 1;
				Main.projectile[bee].GetGlobalProjectile<CalamityGlobalProjectile>().forceMelee = true;
			}
        }
    }
}
