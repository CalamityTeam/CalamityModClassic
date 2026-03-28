using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons.Astral
{
	public class AstralStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Astral Staff");
			// Tooltip.SetDefault("Summons a large crystal from the sky that has a large area of effect on impact.");
            Item.staff[Item.type] = true;
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 180;
	        Item.crit += 15;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 26;
	        Item.width = 86;
	        Item.height = 72;
	        Item.useTime = 35;
	        Item.useAnimation = 35;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 5f;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.rare = 7;
	        Item.UseSound = SoundID.Item105;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("AstralCrystal").Type;
	        Item.shootSpeed = 15f;
	    }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "AstralBar", 6);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 spawnPos = new Vector2(player.MountedCenter.X + Main.rand.Next(-200, 201), player.MountedCenter.Y - 600f);
            Vector2 targetPos = Main.MouseWorld + new Vector2(Main.rand.Next(-30, 31), Main.rand.Next(-30, 31));
            Vector2 velocity2 = targetPos - spawnPos;
            velocity2.Normalize();
            velocity2 *= 13f;

            int p = Projectile.NewProjectile(Entity.GetSource_FromThis(null),spawnPos, velocity2, type, damage, knockback, player.whoAmI);
            Main.projectile[p].ai[0] = targetPos.Y - 120;

            return false;
		}
	}
}