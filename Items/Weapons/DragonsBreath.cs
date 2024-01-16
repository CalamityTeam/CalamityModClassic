using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class DragonsBreath : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/11.Yharon/DragonsBreath");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Dragon's Breath");
        Item.damage = 137;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 64;
        Item.height = 28;
        ////Tooltip.SetDefault("Fires dragon fire blasts at incredible speeds\n80% chance to not consume ammo");
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 6.5f;
        Item.value = 10000000;
        Item.rare = 10;
        Item.UseSound = SoundID.Item36;
        Item.autoReuse = true;
        Item.shoot = 10; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 33f;
        Item.useAmmo = 97;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
	    for (int i = 0; i <= 12; i++)
	    {
	    	float SpeedX = velocity.X + (float) Main.rand.Next(-65, 66) * 0.05f;
	    	float SpeedY = velocity.Y + (float) Main.rand.Next(-65, 66) * 0.05f;
	    	Projectile.NewProjectile(source, position, new Vector2(SpeedX, SpeedY),Mod.Find<ModProjectile>("DragonBurst").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    }
	    return false;
	}
    
    public override bool CanConsumeAmmo(Item ammo, Player player)
    {
    	if (Main.rand.Next(0, 100) <= 80)
    		return false;
    	return true;
    }
}}