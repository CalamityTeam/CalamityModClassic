using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons
{
	public class PurgeGuzzler : ModItem
	{
		public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	    {
		    //texture =("CalamityModClassic1Point1/Items/Weapons/9.Providence/PurgeGuzzler");
		    return true;
	    }
	
	    public override void SetDefaults()
	    {
		    //Tooltip.SetDefault("Purge Guzzler");
			Item.damage = 285;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 6;
			Item.width = 54;
			Item.height = 44;
			////Tooltip.SetDefault("Fires 3 beams of holy energy");
			Item.useTime = 12;
			Item.useAnimation = 12;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 4.5f;
			Item.UseSound = new Terraria.Audio.SoundStyle("CalamityModClassic1Point1/Sounds/Item/LaserCannon");;
			Item.value = 5000000;
			Item.rare = 10;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("HolyLaser").Type; //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 6f;
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	        float SpeedX = velocity.X + 10f * 0.05f;
	        float SpeedY = velocity.Y + 10f * 0.05f;
	        float SpeedX2 = velocity.X - 10f * 0.05f;
	        float SpeedY2 = velocity.Y - 10f * 0.05f;
	        float SpeedX3 = velocity.X + 0f * 0.05f;
	        float SpeedY3 = velocity.Y + 0f * 0.05f;
	        Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY,type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	        Projectile.NewProjectile(source, position.X, position.Y, SpeedX2, SpeedY2, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	        Projectile.NewProjectile(source, position.X, position.Y, SpeedX3, SpeedY3, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
		}
	}
}