using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class SirensSong : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/7.Leviathan/SirensSong");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Siren's Song");
        Item.damage = 63;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 7;
        Item.width = 56;
        Item.height = 50;
        Item.useTime = 12;
        Item.useAnimation = 12;
        Item.useStyle = 5;
        ////Tooltip.SetDefault("Casts slow-moving treble clefs that confuse enemies");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 6.5f;
        Item.value = 750000;
        Item.rare = 7;
        Item.UseSound = SoundID.Item26;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("SirensSong").Type;
        Item.shootSpeed = 13f;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
	    float SpeedX = velocity.X + (float) Main.rand.Next(-35, 36) * 0.05f;
	    float SpeedY = velocity.Y + (float) Main.rand.Next(-35, 36) * 0.05f;
	    int projectile1 = Projectile.NewProjectile(source, position, new Vector2(SpeedX, SpeedY), type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    if (Main.rand.Next(3) == 0)
	    {
	    	Main.projectile[projectile1].penetrate = -1;
	    }
	    return false;
	}
}}