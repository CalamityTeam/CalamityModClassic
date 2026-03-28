using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class EnergyStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Energy Staff");
			Item.staff[Item.type] = true;
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 170;
	        Item.DamageType = DamageClass.Summon;
	        Item.sentry = true;
	        Item.mana = 25;
	        Item.width = 66;
	        Item.height = 68;
	        Item.useTime = 38;
	        Item.useAnimation = 38;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 5f;
            Item.value = Item.buyPrice(1, 20, 0, 0);
            Item.rare = 10;
            Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("ProfanedEnergy").Type;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	    {
            position = Main.MouseWorld;
            velocity.X = 0;
            velocity.Y = 0;
            Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);
            player.UpdateMaxTurrets();
			return false;
	    }
	}
}